using FluentValidation;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Validators;

public class UserGameValidator : AbstractValidator<UserGame>
{
    public UserGameValidator()
    {
        RuleFor(userGame => userGame.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("UserGame Id must be 0 or greater.");

        RuleFor(userGame => userGame.UserId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("UserGame UserId must be 0 or greater.");

        RuleFor(userGame => userGame.GameId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("UserGame GameId must be 0 or greater.");

        RuleFor(userGame => userGame.Game)
            .SetValidator(new GameValidator())
            .WithMessage("Invalid Game details.");

        RuleFor(userGame => userGame.User)
            .SetValidator(new UserValidator())
            .WithMessage("Invalid user details.");
    }
}
