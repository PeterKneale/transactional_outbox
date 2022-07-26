namespace Demo.Core.IntegrationEvents;

public class UserNameChangedIntegrationEvent
{
    public Guid UserId { get; init; }
    
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
}
