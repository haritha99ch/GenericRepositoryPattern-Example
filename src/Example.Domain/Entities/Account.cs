using Example.Domain.Enums;

namespace Example.Domain.Entities;
public class Account : Entity
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required Role Role { get; set; }
    public bool IsVerified { get; set; } = false;
}
