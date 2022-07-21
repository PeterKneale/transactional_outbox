using Demo.Core.Infrastructure;
using MartinCostello.Logging.XUnit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Demo.IntegrationTests;

public class DatabaseFixture : ITestOutputHelperAccessor
{
    public DatabaseFixture()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("testsettings.json")
            .Build();

        using var provider = new ServiceCollection()
            .AddInfrastructure(configuration)
            .AddLogging(builder => builder.AddXUnit(this, x =>
            {
                x.IncludeScopes = true;
                x.Filter = Filter;
            }))
            .BuildServiceProvider();

        provider.ApplyDatabaseMigrations(reset: true);
    }

    private bool Filter(string? arg1, LogLevel arg2)
    {
        return true;
    }

    public ITestOutputHelper? OutputHelper { get; set; }
}