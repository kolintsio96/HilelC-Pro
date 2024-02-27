using AccessToDB;
using Common.Repositories;
using Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly IReaderService _readerService;
        private readonly IReaderRepository _readerRepository;
        private readonly ILogger<ReaderController> _logger;

        public ReaderController(IReaderService readerService, IReaderRepository readerRepository, ILogger<ReaderController> logger)
        {
            this._readerService = readerService;
            this._readerRepository = readerRepository;
            this._logger = logger;
        }

        [Authorize]
        [HttpPost("register")]
        public async Task<IActionResult> Register(ReaderDto readerDto)
        {
            _logger.LogInformation($"Register reader begin process with reader dto - {readerDto}");
            var reader = new Reader()
            {
                Login = readerDto.Login,
                Email = readerDto.Email,
                Password = readerDto.Email,
                Name = readerDto.Name,
                Surname = readerDto.Surname,
                DocumentNumber = readerDto.DocumentNumber,
                DocumentTypeId = readerDto.DocumentId,
                LibrarianId = readerDto.LibrarianId
            };

            var result = await _readerService.Register(reader);
            return Created("api/Reader/" + reader.Id, new { reader, result.token });
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            _logger.LogInformation($"Get reader account by id - {id}");

            var reader = await _readerService.GetReader(id);
            if (reader != null)
                return Ok(reader);

            return NotFound("Account not found!");
        }

        [Authorize]
        [HttpGet("list")]
        public async Task<IActionResult> GetReaders()
        {
            _logger.LogInformation($"Get readers list");

            var readers = await _readerRepository.GetReaders();
            if (readers != null)
                return Ok(readers);

            return NotFound("Accounts not found!");
        }

        [Authorize]
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Delete reader account by id - {id}");
            var reader = await _readerService.GetReader(id);
            if (reader != null)
            {
                await _readerRepository.DeleteReader(reader);
                return Ok(reader);
            }

            return NotFound("Account not found!");
        }

        [Authorize]
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> Update(int id, ReaderDto readerDto)
        {
            _logger.LogInformation($"Update reader begin process with reader dto - {readerDto}");
            var reader = await _readerRepository.GetReader(id);
            if (reader != null)
            {
                reader.Login = readerDto.Login;
                reader.Email = readerDto.Email;
                reader.Password = readerDto.Email;
                reader.Name = readerDto.Name;
                reader.Surname = readerDto.Surname;
                reader.DocumentNumber = readerDto.DocumentNumber;
                reader.DocumentTypeId = readerDto.DocumentId;
                reader.LibrarianId = readerDto.LibrarianId;
            }

            var updatedReader = await _readerRepository.UpdateReader(reader);
            return Created("api/Reader/" + updatedReader.Id, updatedReader);
        }
    }
}
