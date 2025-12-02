using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application.Estates.Commands;

public sealed class EstateCreated(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
