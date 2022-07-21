using Demo.Core.Application.Integration;
using Demo.Core.Domain.Users;
using Demo.Core.Infrastructure.Outbox;
using Demo.Core.IntegrationEvents;
using Newtonsoft.Json;

namespace Demo.Core.Application.Users.Events;

public class UserNameChangedEventHandler : INotificationHandler<UserNameChangedEvent>
{
    private readonly IOutboxRepository _outbox;
    private readonly ILogger<UserNameChangedEventHandler> _logger;

    public UserNameChangedEventHandler(IOutboxRepository outbox, ILogger<UserNameChangedEventHandler> logger)
    {
        _outbox = outbox;
        _logger = logger;
    }

    public Task Handle(UserNameChangedEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User Name Changed: {DomainEvent}", domainEvent.GetType().Name);

        var integrationEvent = new UserDataIntegrationEvent
        {
            UserId = domainEvent.User.Id.Value,
            FirstName = domainEvent.User.Name.FirstName,
            LastName = domainEvent.User.Name.LastName
        };
        var json = JsonConvert.SerializeObject(integrationEvent);
        
        var outboxMessage = new OutboxMessage("user-data-integration-event", json);
         _outbox.Add(outboxMessage);
        return Task.CompletedTask;
    }
}
