namespace Demo.Core.Infrastructure.Persistence;

public interface IDomainEventDispatcher
{
    Task DispatchDomainEvents(DatabaseContext db);
}