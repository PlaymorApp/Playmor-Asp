using FluentValidation;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Validators;

public class NotificationValidator : AbstractValidator<Notification>
{
    public NotificationValidator()
    {
        RuleFor(n => n.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id must be 0 or greater.");

        RuleFor(n => n.RecipientId)
            .GreaterThan(0)
            .WithMessage("RecipientId must be greater than 0");

        RuleFor(n => n.Title)
            .NotEmpty()
            .WithMessage("Title must be set");

        RuleFor(n => n.Content)
            .NotEmpty()
            .WithMessage("Content must be set");

    }
}
