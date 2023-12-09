using FluentValidation;
using Voyager.Application.DTOs.Hotel_DTOs;

namespace Voyager.Application.Validators.HotelValidators
{
    public class HotelCreateDtoValidator : AbstractValidator<HotelCreateDto>
    {
        public HotelCreateDtoValidator()
        {
            RuleFor(hotel => hotel.HotelName)
                .NotEmpty();
            RuleFor(hotel => hotel.Description)
                .NotEmpty()
                .MinimumLength(50)
                .MaximumLength(255);
            
        }
    }
}
