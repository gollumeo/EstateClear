using EstateClear.Application.Estates.Commands;
using EstateClear.Application.Estates.Flows;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;
using EstateClear.Tests.Application;

namespace EstateClear.Tests.Application;

public class AcceptInvitationFlowTests
{
    [Fact]
    public async Task AcceptInvitationShouldActivateParticipantAndInvalidateToken()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var participant = estate.AddPendingParticipant("user@example.com");
        var token = InvitationToken.From(Guid.NewGuid().ToString());
        var estates = new EstatesFake();
        estates.EstatesById[estateId] = estate;
        var invitations = new ParticipantInvitationsFake();
        await invitations.Store(token, participant.Id, estateId);
        var input = new AcceptInvitation(token.Value());
        var flow = new AcceptInvitationFlow(estates, invitations);

        var result = await flow.Execute(input);

        Assert.Equal(ParticipantStatus.Active, estate.Participants().Single().Status);
        Assert.False(invitations.Contains(token));
        Assert.NotNull(result);
        Assert.Equal(participant.Id.Value(), result.ParticipantId.Value());
    }
}
