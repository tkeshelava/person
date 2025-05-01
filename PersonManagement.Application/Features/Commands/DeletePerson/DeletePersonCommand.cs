using MediatR;
using PersonManagment.Application.Abstractions;
using PersonManagment.Application.Abstractions.Repositories;
using PersonManagment.Application.Exceptions;
using PersonManagment.Application.Resources;

namespace PersonManagment.Application.Features.Commands.DeletePerson;

public record DeletePersonCommand(int Id) : IRequest;

public class DeletePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeletePersonCommand>
{
    public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await personRepository.GetByIdAsync(request.Id);
        if (person == null)
            throw new ObjectNotFoundException(ErrorMessages.PersonNotFound);

        personRepository.Delete(person);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}