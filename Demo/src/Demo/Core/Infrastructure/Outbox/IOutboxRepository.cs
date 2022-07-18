using Demo.Core.Application.Integration;

namespace Demo.Core.Infrastructure.Outbox;

public interface IOutboxRepository
{
    void Add(OutboxMessage message);
    Task<OutboxMessage?> GetNext();
}