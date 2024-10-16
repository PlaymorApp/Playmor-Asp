using FluentValidation;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Validators;

public class GameValidator : AbstractValidator<Game>
{
    public GameValidator()
    {
        RuleFor(game => game.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("ID must be the greater than or equal to 0");

        RuleFor(game => game.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage("Title is required.");

        RuleFor(game => game.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Description is required.");

        RuleFor(game => game.Details)
            .NotNull()
            .NotEmpty()
            .WithMessage("Details are required.");

        RuleFor(game => game.Developer)
            .NotNull()
            .NotEmpty()
            .WithMessage("Developers are required");

        RuleFor(game => game.Publisher)
            .NotNull()
            .NotEmpty()
            .WithMessage("Publishers are required");

        RuleFor(game => game.Platforms)
            .NotNull()
            .NotEmpty()
            .WithMessage("Platforms are required");

        RuleFor(game => game.Genres)
            .NotNull()
            .NotEmpty()
            .WithMessage("Genres are required");

        RuleFor(game => game.Modes)
            .NotNull()
            .NotEmpty()
            .WithMessage("Modes are required");

        RuleFor(game => game.Cover)
            .NotNull()
            .NotEmpty()
            .WithMessage("Cover is required.");

        RuleFor(game => game.Artwork)
            .NotNull()
            .NotEmpty()
            .WithMessage("Artwork is required.");


        RuleFor(game => game.ReleaseDates)
            .NotNull()
            .NotEmpty()
            .WithMessage("Release dates are required");

        RuleFor(game => game.WebsiteLinks)
            .NotNull()
            .NotEmpty()
            .WithMessage("Release dates are required");
    }

}
