using FluentValidation;
using PersonManagment.Application.Resources;

namespace PersonManagment.Application.Features.Commands.DeleteConnectedPerson;

public class DeleteConnectedPersonCommandValidator : AbstractValidator<DeleteConnectedPersonCommand>
{
    public DeleteConnectedPersonCommandValidator()
    {
        RuleFor(x => x.PersonId)
            .GreaterThan(0)
            .WithMessage(ErrorMessages.IdShouldBeGreaterThanZero);

          RuleFor(x => x.ConnectedPersonId)
              .GreaterThan(0)
             .WithMessage(ErrorMessages.IdShouldBeGreaterThanZero);
    }
}