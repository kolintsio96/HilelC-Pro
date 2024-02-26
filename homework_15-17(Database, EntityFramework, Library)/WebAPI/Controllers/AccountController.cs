using Common;
using Common.Repositories;
using Common.Services;
using AccessToDB;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.IO.Pipelines;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IReaderRepository _readerRepository;
        private readonly ILibrarianRepository _librarianRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, IReaderRepository readerRepository, ILibrarianRepository librarianRepository, IDocumentRepository documentRepository, ILogger<AccountController> logger)
        {
            this._accountService = accountService;
            this._readerRepository = readerRepository;
            this._librarianRepository = librarianRepository;
            this._documentRepository = documentRepository;
            this._logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            _logger.LogInformation($"Try login by - {loginDto.Email}");
            try
            {
                var result = await _accountService.Login(loginDto.Email, loginDto.Password);
                if (result.reader == null) return NotFound(loginDto.Email);
                return Ok(new { result.reader, result.token });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, ex.Message);
                return Unauthorized(loginDto.Email);
            }
        }

        [AllowAnonymous]
        [HttpPost("register/reader")]
        public async Task<IActionResult> Register(ReaderDto readerDto)
        {
            _logger.LogInformation($"Register begin process with reader dto - {readerDto}");
            var reader = new Reader() {
                Login = readerDto.Login,
                Email = readerDto.Email,
                Password = readerDto.Email,
                Name = readerDto.Name,
                Surname = readerDto.Surname,
                DocumentNumber = readerDto.DocumentNumber,
                DocumentTypeId = readerDto.DocumentId,
                LibrarianId = readerDto.LibrarianId
            };

            var result = await _accountService.Register(reader);
            return Created("api/Account/" + reader.Id, new { reader, result.token });
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            _logger.LogInformation($"Get account by id - {id}");

            var reader = await _readerRepository.GetReader(id);
            if (reader != null)
                return Ok(reader);

            return NotFound("Account not found!");
        }
    }
}
