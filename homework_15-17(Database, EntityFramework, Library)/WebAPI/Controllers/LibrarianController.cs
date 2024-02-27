using Common.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using AccessToDB;
using Common.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrarianController : ControllerBase
    {
        private readonly ILibrarianService _librarianService;
        private readonly ILogger<LibrarianController> _logger;

        public LibrarianController(ILibrarianService librarianService, ILogger<LibrarianController> logger)
        {
            this._librarianService = librarianService;
            this._logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(LibrarianDto librarianDto)
        {
            _logger.LogInformation($"Register librarian begin process with reader dto - {librarianDto}");
            var librarian = new Librarian()
            {
                Login = librarianDto.Login,
                Email = librarianDto.Email,
                Password = librarianDto.Email
            };

            var result = await _librarianService.Register(librarian);
            return Created("api/Librarian" + librarian.Id, new { librarian, result.token });
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetLibrarianAccount(int id)
        {
            _logger.LogInformation($"Get librarian account by id - {id}");

            var reader = await _librarianService.GetLibrarian(id);
            if (reader != null)
                return Ok(reader);

            return NotFound("Account not found!");
        }

        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            _logger.LogInformation($"Get list of librarians");

            var librarians = await _librarianService.GetLibrarians();
            if (librarians != null)
                return Ok(librarians);

            return NotFound("Librarians not found!");
        }
    }
}
