using System.Net.Http.Json;
using ExecutorBoard.SystemTests.TestHost;

namespace ExecutorBoard.SystemTests;

public class SystemPingTests : IClassFixture<PostgreSqlDatabaseFixture>
{
    private readonly PostgreSqlDatabaseFixture _database;

    public SystemPingTests(PostgreSqlDatabaseFixture database)
    {
        _database = database;
    }

    [Fact]
    public async Task GivenEphemeralPostgreSql_WhenPostingSystemPing_ThenPayloadIsPersistedAndReturned()
    {
        // Given an isolated, ephemeral PostgreSQL database bound to the API for this test run
        await using var factory = new CustomWebApplicationFactory(_database.ConnectionString);
        using var client = factory.CreateClient();
        var payload = new { message = "alive" };

        // When issuing the bootstrap-only endpoint to force a write/read round-trip
        var response = await client.PostAsJsonAsync("/system/ping", payload);

        // Then the API should acknowledge the persisted record (will fail until the endpoint + persistence exist)
        response.EnsureSuccessStatusCode();
        var echoed = await response.Content.ReadFromJsonAsync<SystemPingResponse>();
        Assert.NotNull(echoed);
        Assert.False(string.IsNullOrWhiteSpace(echoed!.Id));
        Assert.Equal(payload.message, echoed.Payload);
    }

    private record SystemPingResponse(string Id, string Payload);
}
