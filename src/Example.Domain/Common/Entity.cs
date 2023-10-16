namespace Example.Domain.Common;
public abstract class Entity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public static string Hmm = "";
}
