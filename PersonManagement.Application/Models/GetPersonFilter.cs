using PersonManagement.Domain.Enums;

namespace PersonManagment.Application.Models;

public record GetPersonFilter(
    string? FirstName,
    string? LastName,
    string? PersonalN,
    Gender? Gender,
    int? CityId,
    DateTime? BirthDateFrom,
    DateTime? BirthDateTo,
    string? PhoneNumber,
    string? SearchTerm,
    int PageNumber = 1,
    int PageSize = 10
);