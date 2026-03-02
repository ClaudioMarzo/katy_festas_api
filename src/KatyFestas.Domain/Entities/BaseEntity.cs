namespace KatyFestas.Domain.Entities;

public abstract class BaseEntity : Entity
{
    public DateTime? UpdatedAt { get; protected set; }
    public DateTime? DeletedAt { get; protected set; }

    public bool IsDeleted => DeletedAt.HasValue;

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    public void SetUpdatedAt() => UpdatedAt = DateTime.Now;
    public void SetDeletedAt() => DeletedAt = DateTime.Now;
}