using Common.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(IAuthorRepository authorRepository, ILogger<AuthorController> logger)
        {
            this._authorRepository = authorRepository;
            this._logger = logger;
        }

        [Authorize]
        [HttpGet("list")]
        public async Task<IActionResult> GetList([FromQuery] string? search)
        {
            _logger.LogInformation($"Get author by name");

            var authors = await _authorRepository.GetAuthorsByName(search);
            if (authors != null)
                return Ok(authors);

            return NotFound($"Authors with name {search} not found!");
        }
    }
}
