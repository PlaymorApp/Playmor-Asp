using FluentValidation;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Validators;

public class MessageValidator : AbstractValidator<Message>
{
    public MessageValidator()
    {
        RuleFor(m => m.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id can't be lower than 0");

        RuleFor(m => m.RecipientId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("RecipientId can't be lower than 0");

        RuleFor(m => m.SenderId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("SenderId can't be lower than 0");

        RuleFor(m => m.Content)
            .NotEmpty()
            .MaximumLength(2000)
            .WithMessage("Message can't be empty or longer than 2000 characters");
    }
}
