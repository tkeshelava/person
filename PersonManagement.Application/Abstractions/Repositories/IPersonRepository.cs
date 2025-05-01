using PersonManagement.Domain.Entities;
using PersonManagment.Application.Features.Queries.GetPersonsConnection;
using PersonManagment.Application.Models;

namespace PersonManagment.Application.Abstractions.Repositories;

public interface IPersonRepository : IGenericRepository<Person, int>
{
    Task<IReadOnlyList<Person>> GetListAsync(GetPersonFilter filter);
    Task<IReadOnlyList<PersonConnectionsDto>> GetPersonsConnectionsAsync();
    Task<(IReadOnlyList<Person> Items, int TotalCount)> GetPagedListAsync(GetPersonFilter filter);
}