using FluentValidation;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Validators;

public class CommentScoreValidator : AbstractValidator<CommentScore>
{
    public CommentScoreValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id can't be lower than zero");

        RuleFor(c => c.CommentId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("CommenterId can't be lower than 0");

        RuleFor(c => c.UserId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("GameId can't be lower than zero");

        RuleFor(c => c.Value)
            .Must(value => value == -1 || value == 1)
            .WithMessage("Value must be either -1 or 1");
    }
}
