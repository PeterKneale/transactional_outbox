namespace Demo.Core.Domain.Users;

public class UserNameChangedEvent : BaseEvent
{
    public User User { get; }

    public UserNameChangedEvent(User user)
    {
        User = user;
    }
}