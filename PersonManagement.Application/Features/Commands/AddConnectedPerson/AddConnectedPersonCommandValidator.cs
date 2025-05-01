using FluentValidation;
using PersonManagment.Application.Resources;

namespace PersonManagment.Application.Features.Commands.AddConnectedPerson;

public class AddConnectedPersonCommandValidator : AbstractValidator<AddConnectedPersonCommand>
{
    public AddConnectedPersonCommandValidator()
    {
        RuleFor(x => x.PersonId)
            .GreaterThan(0)
            .WithMessage(ErrorMessages.IdShouldBeGreaterThanZero);

        RuleFor(x => x.ConnectedPersonId)
            .GreaterThan(0)
            .WithMessage(ErrorMessages.IdShouldBeGreaterThanZero);

        RuleFor(x => x.ConnectionType)
            .IsInEnum()
            .WithMessage(ErrorMessages.IncorrectConnectionType);
    }
}