using Microsoft.EntityFrameworkCore;

namespace ExecutorBoard.Api.System.Bootstrap;

/// <summary>
/// System/bootstrap-only DbContext used solely for the walking skeleton.
/// </summary>
public class SystemBootstrapDbContext : DbContext
{
    public SystemBootstrapDbContext(DbContextOptions<SystemBootstrapDbContext> options)
        : base(options)
    {
    }

    public DbSet<SystemPing> SystemPings => Set<SystemPing>();
}

public class SystemPing
{
    public Guid Id { get; set; }
    public string Payload { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
