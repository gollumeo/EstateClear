using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application;

public sealed class ProjectMultipleEstates(ExecutorId executorId)
{
    public ExecutorId ExecutorId { get; } = executorId;
}
