using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application.Estates.Commands;

public sealed class EstateRenamed(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
