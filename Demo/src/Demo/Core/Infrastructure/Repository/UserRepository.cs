using Demo.Core.Application;
using Demo.Core.Application.Contracts;
using Demo.Core.Domain.Users;
using Demo.Core.Infrastructure.Persistence;

namespace Demo.Core.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _db;

    public UserRepository(DatabaseContext db)
    {
        _db = db;
    }

    public async Task Add(User user)
    {
        await _db.Users.AddAsync(user);
    }

    public async Task<IEnumerable<User>> List()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task<User?> Get(UserId userId)
    {
        return await _db.Users.FindAsync(userId);
    }
}