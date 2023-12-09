namespace Voyager.Application.DTOs.Hotel_DTOs
{
    public record HotelGetDto(Guid Id, string HotelName, string Description, float Rating, int ReviewCount);
}
