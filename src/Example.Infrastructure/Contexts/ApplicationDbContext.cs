using Example.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example.Infrastructure.Contexts;
public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Account> Accounts { get; set; } = default!;
    public DbSet<Member> Members { get; set; } = default!;
    public DbSet<Blog> Blogs { get; set; } = default!;
}
