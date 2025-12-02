using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application.Estates.Commands;

public sealed class EstateParticipantAdded(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
