using Common.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrarianController : ControllerBase
    {
        private readonly ILibrarianRepository _librarianRepository;
        private readonly ILogger<LibrarianController> _logger;

        public LibrarianController(ILibrarianRepository librarianRepository, ILogger<LibrarianController> logger)
        {
            this._librarianRepository = librarianRepository;
            this._logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            _logger.LogInformation($"Get list of librarians");

            var librarians = await _librarianRepository.GetLibrarians();
            if (librarians != null)
                return Ok(librarians);

            return NotFound("Documents not found!");
        }
    }
}
