using EstateClear.Application;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;
using EstateClear.Persistence.EF;
using EstateClear.Persistence.EF.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EstateClear.Tests.Persistence;

public class EstatesRepositoryEfTests
{
    [Fact]
    public async Task SaveShouldPersistEstateAndLoadReturnsDomainEntity()
    {
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder<EstateClearDbContext>()
            .UseSqlite(connection)
            .Options;
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());

        await using (var context = new EstateClearDbContext(options))
        {
            context.Database.EnsureCreated();
            IEstates repository = new EstatesRepositoryEf(context);
            var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

            await repository.Save(estate);
        }

        await using (var verificationContext = new EstateClearDbContext(options))
        {
            IEstates verificationRepository = new EstatesRepositoryEf(verificationContext);
            var loaded = await verificationRepository.Load(estateId);

            Assert.NotNull(loaded);
            Assert.Equal(estateId.Value(), loaded!.Id.Value());
            Assert.Equal(executorId.Value(), loaded.ExecutorId.Value());
            Assert.Equal("Estate Alpha", loaded.DisplayName().Value());
        }
    }

    [Fact]
    public async Task ByExecutorShouldReturnOnlyMatchingEstates()
    {
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder<EstateClearDbContext>()
            .UseSqlite(connection)
            .Options;
        var executorA = ExecutorId.From(Guid.NewGuid());
        var executorB = ExecutorId.From(Guid.NewGuid());

        await using (var context = new EstateClearDbContext(options))
        {
            context.Database.EnsureCreated();
            IEstates repository = new EstatesRepositoryEf(context);
            var estateA = Estate.Create(EstateId.From(Guid.NewGuid()), executorA, EstateName.From("Estate Alpha"));
            var estateB = Estate.Create(EstateId.From(Guid.NewGuid()), executorB, EstateName.From("Estate Beta"));
            var estateC = Estate.Create(EstateId.From(Guid.NewGuid()), executorA, EstateName.From("Estate Gamma"));

            await repository.Save(estateA);
            await repository.Save(estateB);
            await repository.Save(estateC);
        }

        await using var verificationContext = new EstateClearDbContext(options);
        IEstates verificationRepository = new EstatesRepositoryEf(verificationContext);

        var estatesForExecutorA = await verificationRepository.ByExecutor(executorA);

        Assert.NotNull(estatesForExecutorA);
        Assert.All(estatesForExecutorA, e => Assert.Equal(executorA.Value(), e.ExecutorId.Value()));
    }
}
