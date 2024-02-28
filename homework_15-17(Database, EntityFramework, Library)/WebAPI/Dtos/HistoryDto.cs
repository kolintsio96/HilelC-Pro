using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos
{
    public record HistoryDto(
       [Required] int BookId,
       [Required] int ReaderId,
       [Required] DateTime TakeDate,
       [Required] int BookingTime,
       DateTime ReturnDate
       );
}
