using PersonManagement.Domain.Enums;

namespace PersonManagement.Domain.Entities;

public class ConnectedPerson : BaseEntity<int>
{
    public ConnectedPerson(int personId, int connectPersonId, ConnectionType connectionType)
    {
        PersonId = personId;
        ConnectPersonId = connectPersonId;
        ConnectionType = connectionType;
    }

    // Parameterless constructor for EF Core
    public ConnectedPerson()
    {
    }

    public int PersonId { get; set; }
    public int ConnectPersonId { get; set; }
    public ConnectionType ConnectionType { get; set; }
    public Person Person { get; set; }
    public Person ConnectPerson { get; set; }
}