using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using PersonManagement.Domain.Entities;
using PersonManagment.Application.Abstractions.Repositories;
using PersonManagment.Application.Features.Queries.GetPersonsConnection;
using PersonManagment.Application.Models;

namespace PersonManagement.Persistence.Repositories;

public class PersonRepository(PersonDbContext dbContext)
    : GenericRepository<Person, int>(dbContext), IPersonRepository
{
    public async Task<IReadOnlyList<Person>> GetListAsync(GetPersonFilter filter)
    {
        var query = dbContext.Persons.AsQueryable();

        if (!string.IsNullOrEmpty(filter.SearchTerm))
        {
            query = query.Where(p =>
                EF.Functions.ILike(p.FirstName, $"%{filter.SearchTerm}%") ||
                EF.Functions.ILike(p.LastName, $"%{filter.SearchTerm}%") ||
                EF.Functions.ILike(p.PersonalN, $"%{filter.SearchTerm}%"));
        }

        if (!string.IsNullOrEmpty(filter.FirstName))
        {
            query = query.Where(p => p.FirstName == filter.FirstName);
        }

        if (!string.IsNullOrEmpty(filter.LastName))
        {
            query = query.Where(p => p.LastName == filter.LastName);
        }

        if (!string.IsNullOrEmpty(filter.PersonalN))
        {
            query = query.Where(p => p.PersonalN == filter.PersonalN);
        }

        if (!string.IsNullOrEmpty(filter.PhoneNumber))
        {
            query = query.Where(p => p.PhoneNumbers.Any(p => p.Number == filter.PhoneNumber));
        }

        if (filter.Gender is not null)
        {
            query = query.Where(p => p.Gender == filter.Gender);
        }

        if (filter.CityId.HasValue)
        {
            query = query.Where(p => p.CityId == filter.CityId.Value);
        }

        if (filter.BirthDateFrom.HasValue)
        {
            query = query.Where(p => p.BirthDate >= filter.BirthDateFrom.Value);
        }

        if (filter.BirthDateTo.HasValue)
        {
            query = query.Where(p => p.BirthDate <= filter.BirthDateTo.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<PersonConnectionsDto>> GetPersonsConnectionsAsync()
    {
        var result = await dbContext.Persons
            .SelectMany(person => person.ConnectedPersons
                .GroupBy(cp => cp.ConnectionType)
                .Select(group => new PersonConnectionsDto(
                    person.Id,
                    $"{person.FirstName} + ' ' + {person.LastName}",
                    group.Count(),
                    group.Key.GetDisplayName()
                )))
            .ToListAsync();

        return result;
    }

    public async Task<(IReadOnlyList<Person> Items, int TotalCount)> GetPagedListAsync(GetPersonFilter filter)
    {
        var query = dbContext.Persons.AsQueryable();

        if (!string.IsNullOrEmpty(filter.SearchTerm))
        {
            query = query.Where(p =>
                EF.Functions.ILike(p.FirstName, $"%{filter.SearchTerm}%") ||
                EF.Functions.ILike(p.LastName, $"%{filter.SearchTerm}%") ||
                EF.Functions.ILike(p.PersonalN, $"%{filter.SearchTerm}%"));
        }

        if (!string.IsNullOrEmpty(filter.FirstName))
        {
            query = query.Where(p => p.FirstName == filter.FirstName);
        }

        if (!string.IsNullOrEmpty(filter.LastName))
        {
            query = query.Where(p => p.LastName == filter.LastName);
        }

        if (!string.IsNullOrEmpty(filter.PersonalN))
        {
            query = query.Where(p => p.PersonalN == filter.PersonalN);
        }

        if (!string.IsNullOrEmpty(filter.PhoneNumber))
        {
            query = query.Where(p => p.PhoneNumbers.Any(p => p.Number == filter.PhoneNumber));
        }

        if (filter.Gender is not null)
        {
            query = query.Where(p => p.Gender == filter.Gender);
        }

        if (filter.CityId.HasValue)
        {
            query = query.Where(p => p.CityId == filter.CityId.Value);
        }

        if (filter.BirthDateFrom.HasValue)
        {
            query = query.Where(p => p.BirthDate >= filter.BirthDateFrom.Value);
        }

        if (filter.BirthDateTo.HasValue)
        {
            query = query.Where(p => p.BirthDate <= filter.BirthDateTo.Value);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}