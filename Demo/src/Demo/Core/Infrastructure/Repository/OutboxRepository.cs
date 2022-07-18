using Demo.Core.Application.Integration;
using Demo.Core.Infrastructure.Outbox;
using Demo.Core.Infrastructure.Persistence;

namespace Demo.Core.Infrastructure.Repository;

public class OutboxRepository : IOutboxRepository
{
    private readonly DatabaseContext _context;

    public OutboxRepository(DatabaseContext context)
    {
        _context = context;
    }

    public void Add(OutboxMessage message)
    {
        _context.OutboxMessages.Add(message);
    }

    public async Task<OutboxMessage?> GetNext()
    {
        return await _context
            .OutboxMessages
            .FirstOrDefaultAsync(x => x.Processed == false);
    }
}