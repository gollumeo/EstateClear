using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;

namespace ExecutorBoard.SystemTests.TestHost;

/// <summary>
/// Ephemeral PostgreSQL per test class (system/bootstrap only).
/// </summary>
public class PostgreSqlDatabaseFixture : IAsyncLifetime
{
    private readonly PostgreSqlTestcontainer _container;

    public PostgreSqlDatabaseFixture()
    {
        var databaseName = $"systemtests_{Guid.NewGuid():N}";
        _container = new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = databaseName,
                Username = "postgres",
                Password = "postgres"
            })
            .WithImage("postgres:16-alpine")
            .WithCleanUp(true)
            .Build();
    }

    public string ConnectionString => _container.ConnectionString;

    public Task InitializeAsync() => _container.StartAsync();

    public Task DisposeAsync() => _container.DisposeAsync().AsTask();
}
