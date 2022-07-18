using Demo.Core.Application.Users.Commands;
using Demo.Core.Application.Users.Queries;
using FluentValidation;

namespace Demo.IntegrationTests.Application.Users;

[Collection(nameof(DatabaseCollection))]
public class CreateUserTests : IClassFixture<ServicesFixture>
{
    private readonly ServicesFixture _services;

    public CreateUserTests(ServicesFixture services, ITestOutputHelper outputHelper)
    {
        services.OutputHelper = outputHelper;
        _services = services;
    }

    [Fact]
    public async Task User_created_with_command_can_be_retrieved_with_query()
    {
        // arrange
        var userId = Guid.NewGuid();
        var firstName = "john";
        var lastName = "smith";

        // act
        await _services.Send(new CreateUser.Command(userId, firstName, lastName));
        var result = await _services.Send(new GetUser.Query(userId));

        // assert
        result.Id.Should().Be(userId);
        result.FirstName.Should().Be(firstName);
        result.LastName.Should().Be(lastName);
    }
    
    [Fact]
    public async Task InvalidRequestsThrow()
    {
        // arrange
        var userId = Guid.Empty;
        var firstName = "";
        var lastName = "";

        // act
        Func<Task> act = async () =>
        {
            await _services.Send(new CreateUser.Command(userId, firstName, lastName));
        };

        // assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}