using Demo.Core.Domain.Common;

namespace Demo.Core.Domain.Users;

public class User : BaseEntity
{
    public UserId Id { get; private init; }

    public Name Name { get; private set; }
    
    private User()
    {
    }

    public User(UserId id, Name name)
    {
        Id = id;
        Name = name;
    }

    public void ChangeName(Name name)
    {
        Name = name;
        AddDomainEvent(new UserNameChangedEvent(this));
    }

    public static User CreateInstance(UserId userId, Name name) => new(userId, name);
}