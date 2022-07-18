using Demo.Core.Application;
using Demo.Core.Application.Contracts;
using Demo.Core.Domain.Common;
using Demo.Core.Domain.Users;
using Demo.Core.Infrastructure.Persistence;
using Demo.Core.Infrastructure.Repository;

namespace Demo.IntegrationTests.Infrastructure.Database;

[Collection(nameof(DatabaseCollection))]
public class GetUserTests : IClassFixture<ServicesFixture>
{
    private readonly IServiceProvider _services;

    public GetUserTests(ServicesFixture services, ITestOutputHelper outputHelper)
    {
        services.OutputHelper = outputHelper;
        _services = services.ServiceProvider;
    }

    [Fact]
    public async Task Added_user_can_be_retrieved_with_get()
    {
        // arrange
        await using var scope1 = _services.CreateAsyncScope();
        var repo = scope1.ServiceProvider.GetRequiredService<IUserRepository>();
        var db = scope1.ServiceProvider.GetRequiredService<DatabaseContext>();
        var userId = UserId.CreateInstance(Guid.NewGuid());
        var name = new Name("john", "smith");

        // act
        var user = User.CreateInstance(userId, name);
        await repo.Add(user);
        await db.SaveChangesAsync();

        // assert
        await using var scope2 = _services.CreateAsyncScope();
        var repo2 = scope2.ServiceProvider.GetRequiredService<IUserRepository>();
        var user2 = await repo2.Get(userId);
        user2.Should().BeEquivalentTo(user);
    }
}