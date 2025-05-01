using MediatR;
using PersonManagment.Application.Abstractions.Repositories;
using PersonManagment.Application.Models;

namespace PersonManagment.Application.Features.Queries.GetPersonList;

public record GetPersonListQuery(GetPersonFilter Filter) : IRequest<PagedList<PersonListDto>>;

public class GetPersonListQueryHandler(IPersonRepository personRepository)
    : IRequestHandler<GetPersonListQuery, PagedList<PersonListDto>>
{
    public async Task<PagedList<PersonListDto>> Handle(GetPersonListQuery request,
        CancellationToken cancellationToken)
    {
        var (persons, totalCount) = await personRepository.GetPagedListAsync(request.Filter);
        
        var personDtos = persons.Select(p => new PersonListDto(
            p.Id,
            p.FirstName,
            p.LastName,
            p.Gender,
            p.PersonalN,
            p.BirthDate,
            p.CityId,
            p.City?.Name ?? string.Empty
        )).ToList();
        
        return new PagedList<PersonListDto>(
            personDtos, 
            totalCount, 
            request.Filter.PageNumber, 
            request.Filter.PageSize
        );
    }
}