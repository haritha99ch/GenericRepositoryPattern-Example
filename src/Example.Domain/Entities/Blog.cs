namespace Example.Domain.Entities;
public class Blog : Entity
{
    public Guid MemberId { get; set; }
    [Required]
    public required string Caption { get; set; }
    [Required]
    public required string Description { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [Required]
    public bool AgeRestricted { get; set; } = false;

    [ForeignKey(nameof(MemberId))]
    public Member? Member { get; set; }
}
