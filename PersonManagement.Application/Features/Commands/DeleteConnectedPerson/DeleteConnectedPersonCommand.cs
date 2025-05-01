using MediatR;
using PersonManagment.Application.Abstractions;
using PersonManagment.Application.Abstractions.Repositories;
using PersonManagment.Application.Exceptions;
using PersonManagment.Application.Resources;

namespace PersonManagment.Application.Features.Commands.DeleteConnectedPerson;

public record DeleteConnectedPersonCommand(int PersonId, int ConnectedPersonId) : IRequest;

public class DeleteConnectedPersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteConnectedPersonCommand>
{
    public async Task Handle(DeleteConnectedPersonCommand command, CancellationToken cancellationToken)
    {
        var person = await personRepository.GetByIdAsync(command.PersonId, x => x.ConnectedPersons);
        if (person == null)
            throw new ObjectNotFoundException(ErrorMessages.PersonNotFound);
        var connectedPerson = person.ConnectedPersons.FirstOrDefault(x =>
            x.ConnectPersonId == command.ConnectedPersonId);

        if (connectedPerson == null)
            throw new ObjectNotFoundException(ErrorMessages.PersonNotFound);

        person.ConnectedPersons.Remove(connectedPerson);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}