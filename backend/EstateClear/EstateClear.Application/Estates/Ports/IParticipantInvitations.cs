using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application.Estates.Ports;

public interface IParticipantInvitations
{
    Task Store(InvitationToken token, ParticipantId participantId);
}
