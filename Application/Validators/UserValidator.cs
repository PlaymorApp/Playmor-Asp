using FluentValidation;
using Playmor_Asp.Domain.Enums;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Id)
           .GreaterThanOrEqualTo(0)
           .WithMessage("ID must be the greater than or equal to 0");

        RuleFor(user => user.Username)
           .NotEmpty()
           .MaximumLength(100);

        RuleFor(user => user.Email)
            .NotEmpty()
            .MaximumLength(100)
            .EmailAddress();

        RuleFor(user => user.PhoneNumber)
            .MaximumLength(9);

        RuleFor(user => user.UserRole)
            .IsInEnum().WithMessage("Invalid user role.")
            .Must(role => role == UserRole.User || role == UserRole.Admin)
            .WithMessage("User role must be either User or Admin.");
    }
}
