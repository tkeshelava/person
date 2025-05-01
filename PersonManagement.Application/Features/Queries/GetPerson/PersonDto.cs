using PersonManagement.Domain.Enums;

namespace PersonManagment.Application.Features.Queries.GetPerson;

public record PersonDto(int Id, string FirstName, string LastName, Gender Gender, string PersonalN, DateTime BirthDate,
    int CityId, string City, string ImageUrl);