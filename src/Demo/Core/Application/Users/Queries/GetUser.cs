using Demo.Core.Application.Contracts;
using Demo.Core.Domain.Users;
using FluentValidation;

namespace Demo.Core.Application.Users.Queries;

public static class GetUser
{
    public record Query(Guid UserId) : IRequest<Result>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.UserId).NotEmpty();
        }
    }
    
    public record Result(Guid Id, string FirstName, string LastName, string FullName);

    private class Handler : IRequestHandler<Query, Result>
    {
        private readonly IUserRepository _users;

        public Handler(IUserRepository users)
        {
            _users = users;
        }

        public async Task<Result> Handle(Query query, CancellationToken token)
        {
            var userId = UserId.CreateInstance(query.UserId);
            var user = await _users.Get(userId);
            if (user == null)
            {
                throw new NotImplementedException();
            }

            return new Result(user.Id.Value, user.Name.FirstName, user.Name.LastName, user.Name.FirstName);
        }
    }
}