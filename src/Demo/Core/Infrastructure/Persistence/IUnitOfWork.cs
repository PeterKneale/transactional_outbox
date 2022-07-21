namespace Demo.Core.Infrastructure.Persistence;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
}