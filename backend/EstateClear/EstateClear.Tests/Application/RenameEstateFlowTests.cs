using System;
using System.Threading.Tasks;
using EstateClear.Application;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;
using Xunit;

namespace EstateClear.Tests.Application;

public class RenameEstateFlowTests
{
    [Fact]
    public async Task RenameEstateFlowShouldUseDomainAggregateForRenaming()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var aggregate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var input = new RenameEstate(estateId, "Estate Beta");
        var estates = new EstatesFake
        {
            LoadedEstate = aggregate
        };
        var flow = new RenameEstateFlow(estates);

        var result = await flow.Execute(input);

        Assert.NotNull(result);
        Assert.Single(estates.SavedEstates);
        Assert.Equal("Estate Beta", estates.SavedEstates[0].DisplayName().Value());
        Assert.Empty(estates.RenamedEstates);
    }
}
