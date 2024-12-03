using FluentValidation;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Validators;

public class CommentValidator : AbstractValidator<Comment>
{
    public CommentValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id can't be lower than zero");

        RuleFor(c => c.CommenterId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("CommenterId can't be lower than 0");

        RuleFor(c => c.GameId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("GameId can't be lower than zero");

        RuleFor(c => c.Content)
            .MaximumLength(500)
            .WithMessage("Content can't be longer than 500 characters");
    }
}
