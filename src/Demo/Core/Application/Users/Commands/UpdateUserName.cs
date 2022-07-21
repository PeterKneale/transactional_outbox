using Demo.Core.Application.Contracts;
using Demo.Core.Domain.Common;
using Demo.Core.Domain.Users;
using FluentValidation;

namespace Demo.Core.Application.Users.Commands;

public static class UpdateUserName
{
    public record Command(Guid UserId, string FirstName, string LastName) : IRequest;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(m => m.LastName).NotEmpty().MaximumLength(50);
        }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly IUserRepository _users;

        public Handler(IUserRepository users)
        {
            _users = users;
        }

        public async Task<Unit> Handle(Command command, CancellationToken token)
        {
            var userid = UserId.CreateInstance(command.UserId);
            var user= await _users.Get(userid);
            if (user == null)
            {
                throw new NotImplementedException();
            }

            var name = new Name(command.FirstName, command.LastName);
            user.ChangeName(name);
            return Unit.Value;
        }
    }
}