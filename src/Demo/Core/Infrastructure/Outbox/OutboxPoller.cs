using Demo.Core.Infrastructure.Persistence;

namespace Demo.Core.Infrastructure.Outbox;

public class OutboxPoller : BackgroundService
{
    private readonly IServiceScopeFactory _scopes;
    private readonly ILogger<OutboxPoller> _logs;

    public OutboxPoller(IServiceScopeFactory scopes, ILogger<OutboxPoller> logs)
    {
        _scopes = scopes;
        _logs = logs;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await PublishMessages(stoppingToken);
        }
    }
    private async Task PublishMessages(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopes.CreateScope();
            
            var work = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var repository = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();

            // get message
            await work.BeginTransactionAsync();
            var message = await repository.GetNext();
            if (message != null)
            {
                // publish _message
                _logs.LogInformation($"{message.Type} {message.Data}");
                message.MarkProcessed();
                await work.CommitTransactionAsync();
            }
            else
            {
                // sleep until next poll
                await Task.Delay(1000, stoppingToken);    
            }
        }
    }
}