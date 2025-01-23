using FluentValidation;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Validators;

public class FriendValidator : AbstractValidator<Friend>
{
    public FriendValidator()
    {
        RuleFor(n => n.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id must be 0 or greater.");

        RuleFor(f => f.FirstUserId)
            .GreaterThan(0)
            .WithMessage("FirstUserId must be greater than 0");

        RuleFor(f => f.SecondUserId)
            .GreaterThan(0)
            .WithMessage("SecondUserId must be greater than 0");
    }
}