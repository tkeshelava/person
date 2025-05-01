using MediatR;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;
using PersonManagment.Application.Abstractions;
using PersonManagment.Application.Abstractions.Repositories;

namespace PersonManagment.Application.Features.Commands.AddPerson;

public record AddPersonCommand(
    string FirstName,
    string LastName,
    Gender GenderId,
    string PersonalN,
    DateTime BirthDate,
    int CityId,
    List<PhoneNumberDto> Phones) : IRequest;

public class AddPersonCommandHandler(IPersonRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<AddPersonCommand>
{
    public async Task Handle(AddPersonCommand command, CancellationToken cancellationToken)
    {
        var phoneNumbers = command.Phones.Select(phone => new PhoneNumber(phone.Type, phone.Value)).ToList();

        var person = new Person(
            command.FirstName,
            command.LastName,
            command.GenderId,
            command.PersonalN,
            command.BirthDate,
            command.CityId,
            phoneNumbers
        );

        await repository.AddAsync(person);
        await unitOfWork.SaveChangesAsync(cancellationToken);

    }
}

public record PhoneNumberDto(PhoneNumberType Type, string Value);