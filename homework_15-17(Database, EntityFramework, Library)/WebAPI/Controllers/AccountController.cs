using AccessToDB;
using Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            this._accountService = accountService;
            this._logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            _logger.LogInformation($"Try login by - {loginDto.Email}");
            try
            {
                var result = await _accountService.Login(loginDto.Email, loginDto.Password, loginDto.IsReader);
                if (result.account == null) return NotFound(loginDto.Email);
                return Ok(new { result.account, result.token });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, ex.Message);
                return Unauthorized(loginDto.Email);
            }
        }
    }
}
