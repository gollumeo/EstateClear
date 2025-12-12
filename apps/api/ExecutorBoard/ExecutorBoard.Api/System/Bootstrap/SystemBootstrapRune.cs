using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rituals.Contracts;

namespace ExecutorBoard.Api.System.Bootstrap;

/// <summary>
/// System/bootstrap-only wiring to satisfy the walking skeleton test.
/// </summary>
public class SystemBootstrapRune : IRune
{
    public void Extract(IServiceCollection services, IConfiguration config)
    {
        var connectionString = config["SystemBootstrap:ConnectionString"];
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            return;
        }

        services.AddDbContext<SystemBootstrapDbContext>(options =>
            options.UseNpgsql(connectionString));
    }

    public void CarveWith(WebApplication app)
    {
        var connectionString = app.Configuration["SystemBootstrap:ConnectionString"];
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            return;
        }

        EnsureDatabaseCreated(app);

        app.MapPost("/system/ping", async (SystemPingRequest request, SystemBootstrapDbContext db) =>
        {
            var payload = string.IsNullOrWhiteSpace(request.Message) ? "alive" : request.Message!;
            var ping = new SystemPing
            {
                Id = Guid.NewGuid(),
                Payload = payload,
                CreatedAt = DateTime.UtcNow
            };

            db.SystemPings.Add(ping);
            await db.SaveChangesAsync();

            var readBack = await db.SystemPings.FindAsync(ping.Id);
            return Results.Ok(new SystemPingResponse(readBack!.Id.ToString(), readBack.Payload));
        });
    }

    private static void EnsureDatabaseCreated(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<SystemBootstrapDbContext>();
        db.Database.EnsureCreated();
    }

    private sealed record SystemPingRequest(string? Message);
    private sealed record SystemPingResponse(string Id, string Payload);
}
