using EstateClear.Persistence.EF.Configurations;
using EstateClear.Persistence.EF.Records;
using Microsoft.EntityFrameworkCore;

namespace EstateClear.Persistence.EF;

public sealed class EstateClearDbContext(DbContextOptions<EstateClearDbContext> options) : DbContext(options)
{
    public DbSet<UserRecord> Users => Set<UserRecord>();
    public DbSet<EstateRecord> Estates => Set<EstateRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserRecordConfiguration());
        modelBuilder.ApplyConfiguration(new EstateRecordConfiguration());
    }
}
