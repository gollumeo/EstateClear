using EstateClear.Application.Estates.Commands;
using EstateClear.Application.Estates.Ports;
using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application.Estates.Flows;

public sealed class AcceptInvitationFlow(IEstates estates, IParticipantInvitations invitations)
{
    public async Task<AcceptInvitationResult> Execute(AcceptInvitation input)
    {
        var token = InvitationToken.From(input.Token);
        var lookup = await invitations.FindByToken(token);
        if (lookup is null)
        {
            throw new Exception("Invalid token");
        }

        var estate = await estates.Load(lookup.EstateId);
        if (estate is null)
        {
            throw new Exception("Estate not found");
        }

        var participant = estate.ActivateParticipant(lookup.ParticipantId);

        await invitations.Remove(token);
        await estates.Save(estate);

        return new AcceptInvitationResult(participant.Id);
    }
}

public sealed class AcceptInvitationResult(ParticipantId participantId)
{
    public ParticipantId ParticipantId { get; } = participantId;
}
