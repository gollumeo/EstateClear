using EstateClear.Domain;
using EstateClear.Domain.Estates;

namespace EstateClear.Tests.Domain;

public class EstateCreationTests
{
    [Fact]
    public void CreateEstateWithExecutorAndDisplayNameSucceeds()
    {
        // Arrange
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var displayName = "Estate Alpha";

        // Act
        var estate = Estate.Create(estateId, executorId, displayName);

        // Assert
        Assert.NotNull(estate);
        Assert.Equal(estateId, estate.Id);
        Assert.Equal(executorId, estate.ExecutorId);
        Assert.Equal(displayName, estate.DisplayName);
        Assert.Equal(EstateStatus.Active, estate.Status);
        Assert.Empty(estate.Participants);
        Assert.Empty(estate.Updates);
        Assert.Empty(estate.Documents);
        Assert.Empty(estate.Milestones);
        Assert.Empty(estate.Reminders);
    }

    [Fact]
    public void CreateEstateWithMissingExecutorThrows()
    {
        // Arrange
        var estateId = EstateId.From(Guid.NewGuid());
        ExecutorId? executorId = null;
        var displayName = "Estate Alpha";

        // Act
        var exception = Assert.Throws<DomainException>(() => Estate.Create(estateId, executorId!, displayName));

        // Assert
        Assert.Equal("Executor is required", exception.Message);
    }

    [Fact]
    public void CreateEstateWithEmptyDisplayNameThrows()
    {
        // Arrange
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var displayName = "   ";

        // Act
        var exception = Assert.Throws<DomainException>(() => Estate.Create(estateId, executorId, displayName));

        // Assert
        Assert.Equal("Display name is required", exception.Message);
    }
}
