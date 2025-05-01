using MediatR;
using PersonManagment.Application.Abstractions.Repositories;

namespace PersonManagment.Application.Features.Queries.GetPersonsConnection;

public record GetPersonsConnectionQuery() : IRequest<IReadOnlyList<PersonConnectionsDto>>;

public class GetPersonsConnectionQueryHandler(IPersonRepository personRepository) : IRequestHandler<GetPersonsConnectionQuery, IReadOnlyList<PersonConnectionsDto>>
{
    public async Task<IReadOnlyList<PersonConnectionsDto>> Handle(GetPersonsConnectionQuery request, CancellationToken cancellationToken)
    {
        return  await personRepository.GetPersonsConnectionsAsync();
    }
}


