using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application;

public sealed class CloseEstate(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
