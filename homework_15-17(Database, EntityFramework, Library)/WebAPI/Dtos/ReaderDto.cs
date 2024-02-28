using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos
{
    public record ReaderDto(
       [Required] string Login,
       [Required][StringLength(maximumLength: 255, MinimumLength = 3)] string Password,
       [Required][EmailAddress] string Email,
       string? Name,
       string? Surname,
       [Required] int DocumentId,
       string? DocumentNumber,
       [Required] int LibrarianId
       );
}
