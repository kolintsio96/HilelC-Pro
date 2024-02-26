using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos
{
    public record LoginDto(
        [Required][EmailAddress] string Email,
        [Required][StringLength(maximumLength: 255, MinimumLength = 3)] string Password
        );
}
