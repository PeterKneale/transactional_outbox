using Demo.Core.Application.Users.Commands;
using Demo.Core.Application.Users.Queries;

namespace Demo.IntegrationTests.Application.Users;

[Collection(nameof(DatabaseCollection))]
public class UpdateNameTests : IClassFixture<ServicesFixture>
{
    private readonly ServicesFixture _services;

    public UpdateNameTests(ServicesFixture services, ITestOutputHelper outputHelper)
    {
        services.OutputHelper = outputHelper;
        _services = services;
    }

    [Fact]
    public async Task User_name_updated_with_command_can_be_retrieved_with_query()
    {
        // arrange
        var userId = Guid.NewGuid();
        var firstName = "john";
        var lastName = "smith";
        var firstNameUpdated = "john-Updated";
        var lastNameUpdated = "smith-Updated";

        // act
        await _services.Send(new CreateUser.Command(userId, firstName, lastName));
        await _services.Send(new UpdateUserName.Command(userId, firstNameUpdated, lastNameUpdated));

        // assert
        var result = await _services.Send(new GetUser.Query(userId));
        result.Id.Should().Be(userId);
        result.FirstName.Should().Be(firstNameUpdated);
        result.LastName.Should().Be(lastNameUpdated);
    }
}