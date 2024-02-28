using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos
{
    public record BookDto(
       [Required] string Name,
       string PublishKey,
       [Required] int PublishingHousesType,
       int Year,
       string Country,
       string City,
       [Required] int BookingTime
       );
}
