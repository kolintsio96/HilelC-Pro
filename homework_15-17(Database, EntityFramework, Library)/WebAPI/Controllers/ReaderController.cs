using AccessToDB;
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
        private readonly ILogger<ReaderController> _logger;

        public ReaderController(IReaderService readerService, ILogger<ReaderController> logger)
        {
            this._readerService = readerService;
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
        public async Task<IActionResult> GetReaderAccount(int id)
        {
            _logger.LogInformation($"Get reader account by id - {id}");

            var reader = await _readerService.GetReader(id);
            if (reader != null)
                return Ok(reader);

            return NotFound("Account not found!");
        }
    }
}
