using MediatR;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;
using PersonManagment.Application.Abstractions;
using PersonManagment.Application.Abstractions.Repositories;
using PersonManagment.Application.Features.Commands.AddPerson;

namespace PersonManagment.Application.Features.Commands.UpdatePerson;

public record UpdatePersonCommand(
    int Id,
    string FirstName,
    string LastName,
    Gender GenderId,
    string PersonalN,
    DateTime BirthDate,
    int CityId,
    List<PhoneNumberDto> Phones) : IRequest;

public class UpdatePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdatePersonCommand>
{
    public async Task Handle(UpdatePersonCommand command, CancellationToken cancellationToken)
    {
        var person = await personRepository.GetByIdAsync(command.Id, x => x.PhoneNumbers);
        if (person == null)
        {
            throw new KeyNotFoundException("Person not found.");
        }

        person.FirstName = command.FirstName;
        person.LastName = command.LastName;
        person.Gender = command.GenderId;
        person.PersonalN = command.PersonalN;
        person.BirthDate = command.BirthDate;
        person.CityId = command.CityId;

        person.PhoneNumbers.Clear();
        var updatedPhoneNumbers = command.Phones.Select(phone => new PhoneNumber(phone.Type, phone.Value)).ToList();
        foreach (var phoneNumber in updatedPhoneNumbers)
        {
            person.PhoneNumbers.Add(phoneNumber);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

    }
}