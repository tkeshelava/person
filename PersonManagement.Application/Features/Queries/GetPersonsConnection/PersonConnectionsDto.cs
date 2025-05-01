namespace PersonManagment.Application.Features.Queries.GetPersonsConnection;

public record PersonConnectionsDto(
    int PersonId,
    string FullName,
    int Count,
    string ConnectionType
);