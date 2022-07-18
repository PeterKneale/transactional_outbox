using Demo.Core.Application.Integration;
using Demo.Core.Domain.Users;
using Demo.Core.Infrastructure.Database;

namespace Demo.Core.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}