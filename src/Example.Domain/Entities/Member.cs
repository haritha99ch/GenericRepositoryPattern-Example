namespace Example.Domain.Entities;
public class Member : Entity
{
    public Guid AccountId { get; set; }
    [Required]
    public required string FirstName { get; set; }
    [Required]
    public required string LastName { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [Required]
    public required string PhoneNumber { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    [NotMapped]
    public int Age => DateTime.Now.Year - DateOfBirth.Year;

    [ForeignKey(nameof(AccountId))]
    public Account? Account { get; set; }
}
