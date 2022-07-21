namespace Demo.Core.IntegrationEvents;

public class UserDataIntegrationEvent
{
    public Guid UserId { get; init; }
    
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
}
