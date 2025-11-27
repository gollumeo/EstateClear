using EstateClear.Domain.Estates.ValueObjects;
using EstateClear.Domain.Estates.Entities;

namespace EstateClear.Application;

public interface IEstates
{
    Task Add(EstateId estateId, ExecutorId executorId, string displayName);

    Task<bool> ExistsWithName(ExecutorId executorId, EstateName estateName);

    Task Rename(EstateId estateId, EstateName newName);

    Task<ExecutorId> Executor(EstateId estateId);

    Task<EstateName> NameOf(EstateId estateId);

    Task<Estate?> Load(EstateId estateId);

    Task Save(Estate estate);
}
