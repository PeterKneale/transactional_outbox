using Demo.Core.Application.Integration;
using Demo.Core.Domain.Users;
using Demo.Core.Infrastructure.Outbox;
using Demo.Core.IntegrationEvents;
using Newtonsoft.Json;

namespace Demo.Core.Application.Users.Events;

public class UserNameChangedEventHandler : INotificationHandler<UserNameChangedEvent>
{
    private readonly IOutboxRepository _outbox;

    public UserNameChangedEventHandler(IOutboxRepository outbox)
    {
        _outbox = outbox;
    }

    public Task Handle(UserNameChangedEvent domainEvent, CancellationToken cancellationToken)
    {
        var json = JsonConvert.SerializeObject(new UserNameChangedIntegrationEvent
        {
            UserId = domainEvent.User.Id.Value,
            FirstName = domainEvent.User.Name.FirstName,
            LastName = domainEvent.User.Name.LastName
        });
        
        _outbox.Add(new OutboxMessage(nameof(UserNameChangedIntegrationEvent), json));
        
        return Task.CompletedTask;
    }
}
