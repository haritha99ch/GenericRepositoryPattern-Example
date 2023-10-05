namespace Example.Domain.Entities;
public class Member : Entity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime CreatedAt { get; set; } = DateTime.Now;
    public required string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Age => DateTime.Now.Year - DateOfBirth.Year;
}
