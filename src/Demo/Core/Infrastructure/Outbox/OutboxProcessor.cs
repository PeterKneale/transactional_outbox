using Amazon.SQS;
using Amazon.SQS.Model;
using Demo.Core.Application.Integration;
using Demo.Core.Infrastructure.Persistence;

namespace Demo.Core.Infrastructure.Outbox;

public class OutboxProcessor : BackgroundService
{
    private readonly IServiceScopeFactory _scopes;
    private readonly IAmazonSQS _sqs;
    private readonly ILogger<OutboxProcessor> _logs;

    public OutboxProcessor(IServiceScopeFactory scopes, IAmazonSQS sqs, ILogger<OutboxProcessor> logs)
    {
        _scopes = scopes;
        _sqs = sqs;
        _logs = logs;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queueUrl = await GetQueueUrl(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopes.CreateScope();
            
            var work = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var repository = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();

            _logs.LogDebug("Polling...");
            await work.BeginTransactionAsync();
            var message = await repository.GetNext();
            if (message == null)
            {
                await Task.Delay(5000, stoppingToken);
                continue;
            }
            
            _logs.LogInformation("Publishing {MessageType} {MessageData}", message.Type, message.Data);
            await _sqs.SendMessageAsync(GetSendMessageRequest(queueUrl, message), stoppingToken);
            message.MarkProcessed();
            await work.CommitTransactionAsync();
        }
    }
    
    private static SendMessageRequest GetSendMessageRequest(string queueUrl, OutboxMessage message) => new()
    {
        QueueUrl = queueUrl,
        MessageBody = message.Data
    };
    
    private async Task<string> GetQueueUrl(CancellationToken stoppingToken)
    {
        var queueUrlResponse = await _sqs.GetQueueUrlAsync("outbox-demo-queue", stoppingToken);
        return queueUrlResponse.QueueUrl;
    }
}