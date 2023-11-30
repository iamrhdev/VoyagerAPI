using FluentValidation;
using Voyager.Application.DTOs.Auth_DTOs;

namespace Voyager.Application.Validators.AuthValidators
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty()
                .MaximumLength(30)
                .Matches("^[a-zA-Z]+$")
                .WithMessage("First name must contain only letters and have a maximum length of 30 characters.");

            RuleFor(user => user.LastName)
                .NotEmpty()
                .MaximumLength(50)
                .Matches("^[a-zA-Z]+$")
                .WithMessage("Last name must contain only letters and have a maximum length of 50 characters.");

            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Valid email is required");

            RuleFor(user => user.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]")
                .WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[0-9]")
                .WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("Password must contain at least one special character.");

            RuleFor(user => user.PasswordConfirm)
                .NotEmpty()
                .Equal(user => user.Password)
                .MinimumLength(8)
                .Matches("[A-Z]")
                .WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[0-9]")
                .WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("Password must contain at least one special character.");
        }
    }
}
