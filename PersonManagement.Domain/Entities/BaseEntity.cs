namespace PersonManagement.Domain.Entities;

public class BaseEntity
{
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class BaseEntity<T> : BaseEntity
{
    public T Id { get; set; }
}