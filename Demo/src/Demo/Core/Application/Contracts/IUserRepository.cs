using Demo.Core.Domain.Users;

namespace Demo.Core.Application.Contracts;

public interface IUserRepository
{
    Task Add(User user);
    Task<IEnumerable<User>> List();
    Task<User?> Get(UserId userId);
}