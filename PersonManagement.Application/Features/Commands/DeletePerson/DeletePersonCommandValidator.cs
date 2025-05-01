using FluentValidation;
using PersonManagment.Application.Resources;

namespace PersonManagment.Application.Features.Commands.DeletePerson;

public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
{
    public DeletePersonCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage(ErrorMessages.IdShouldBeGreaterThanZero);  
    }
}