using MediatR;
using PersonManagment.Application.Abstractions;
using PersonManagment.Application.Abstractions.Repositories;
using PersonManagment.Application.Exceptions;
using PersonManagment.Application.Resources;

namespace PersonManagment.Application.Features.Queries.GetPerson;

public record GetPersonQuery(int Id) : IRequest<PersonDto>;

public class GetPersonQueryHandler(
    IPersonRepository personRepository,
    IStorageClient storageClient)
    : IRequestHandler<GetPersonQuery, PersonDto>
{
    public async Task<PersonDto> Handle(GetPersonQuery query, CancellationToken cancellationToken)
    {
        var person = await personRepository.GetByIdAsync(query.Id, x => x.City);

        if (person == null)
        {
            throw new ObjectNotFoundException(ErrorMessages.PersonNotFound);
        }

        string imageUrl = string.Empty;
        if (!string.IsNullOrEmpty(person.ImageUrl))
        {
            string fileName = Path.GetFileName(person.ImageUrl);
            imageUrl = storageClient.GetFileUrl(fileName);
        }

        return new PersonDto(
            person.Id,
            person.FirstName,
            person.LastName,
            person.Gender,
            person.PersonalN,
            person.BirthDate,
            person.CityId,
            person.City?.Name ?? string.Empty,
            imageUrl
        );
    }
}