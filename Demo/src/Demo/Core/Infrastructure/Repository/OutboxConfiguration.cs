using Demo.Core.Application.Integration;

namespace Demo.Core.Infrastructure.Repository;

public class OutboxConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Id);
        builder.Property(e => e.Type);
        builder.Property(e => e.Data);
        builder.Property(e => e.Processed);
    }
}