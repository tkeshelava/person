using MediatR;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;
using PersonManagment.Application.Abstractions;
using PersonManagment.Application.Abstractions.Repositories;
using PersonManagment.Application.Exceptions;
using PersonManagment.Application.Resources;

namespace PersonManagment.Application.Features.Commands.AddConnectedPerson;

public record AddConnectedPersonCommand(int PersonId, int ConnectedPersonId, ConnectionType ConnectionType) : IRequest;

public class AddConnectedPersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<AddConnectedPersonCommand>
{
    public async Task Handle(AddConnectedPersonCommand command, CancellationToken cancellationToken)
    {
        var person = await personRepository.GetByIdAsync(command.PersonId, x => x.ConnectedPersons);
        if (person == null)
            throw new ObjectNotFoundException(ErrorMessages.PersonNotFound);

        var connectedPerson = await personRepository.GetByIdAsync(command.ConnectedPersonId);
        if (connectedPerson == null)
            throw new ObjectNotFoundException(ErrorMessages.PersonNotFound);

        var connectionPerson = new ConnectedPerson(command.PersonId, command.ConnectedPersonId, command.ConnectionType);
        person.ConnectedPersons.Add(connectionPerson);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}