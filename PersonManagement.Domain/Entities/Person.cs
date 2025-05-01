using PersonManagement.Domain.Enums;

namespace PersonManagement.Domain.Entities;

public class Person : BaseEntity<int>
{
    public Person(string firstName, string lastName, Gender gender, string personalN, DateTime birthDate, int cityId, List<PhoneNumber> phones)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        PersonalN = personalN;
        BirthDate = birthDate;
        CityId = cityId;
        PhoneNumbers = phones;
    }

    public Person()
    {
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public string PersonalN { get; set; }
    public DateTime BirthDate { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }
    public string? ImageUrl { get; set; }
    public IList<PhoneNumber> PhoneNumbers { get; set; }
    public IList<ConnectedPerson> ConnectedPersons { get; set; }
}