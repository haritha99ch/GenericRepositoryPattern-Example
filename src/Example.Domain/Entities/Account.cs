using Example.Domain.Enums;

namespace Example.Domain.Entities;
public class Account : Entity
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
    [Required]
    public required Role Role { get; set; }
    [Required]
    public bool IsVerified { get; set; }
}
