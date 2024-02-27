using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos
{
    public record LibrarianDto(
       [Required] string Login,
       [Required][StringLength(maximumLength: 255, MinimumLength = 3)] string Password,
       [Required][EmailAddress] string Email
       );
}
