namespace Example.Domain.Entities;
public class Blog : Entity
{
    public required string Caption { get; set; }
    public required string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool AgeRestricted { get; set; } = false;
}
