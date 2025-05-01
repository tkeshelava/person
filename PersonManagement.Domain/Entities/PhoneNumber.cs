using PersonManagement.Domain.Enums;

namespace PersonManagement.Domain.Entities;

public class PhoneNumber : BaseEntity<int>
{
    public PhoneNumber(PhoneNumberType phoneNumberType, string number)
    {
        PhoneNumberType = phoneNumberType;
        Number = number;
    }

    public PhoneNumber()
    {
    }

    public PhoneNumberType PhoneNumberType { get; set; }
    public string Number { get; set; }
    
    public int PersonId { get; set; }
    public Person Person { get; set; }
}