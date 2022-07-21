using Demo.Core.Application.Contracts;
using Demo.Core.Domain.Users;

namespace Demo.Core.Application.Users.Queries;

public static class ListUsers
{
    public record Query : IRequest<Result>;

    public record Result(IEnumerable<Model> Users);

    public record Model(Guid Id, string Name);

    private static class Mapping
    {
        public static Model Map(User user)
        {
            return new Model(user.Id.Value, $"{user.Name.FirstName}");
        }
    }

    private class Handler : IRequestHandler<Query, Result>
    {
        private readonly IUserRepository _users;

        public Handler(IUserRepository users)
        {
            _users = users;
        }

        public async Task<Result> Handle(Query message, CancellationToken token)
        {
            var users = await _users.List();
            var models = users.Select(Mapping.Map);
            return new Result(models);
        }
    }
}