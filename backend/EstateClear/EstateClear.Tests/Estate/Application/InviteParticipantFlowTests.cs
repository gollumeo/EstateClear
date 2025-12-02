using EstateClear.Application.Estates.Commands;
using EstateClear.Application.Estates.Flows;
using EstateClear.Application.Estates.Ports;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;
using EstateClear.Tests.Application;

namespace EstateClear.Tests.Application;

public class InviteParticipantFlowTests
{
    [Fact]
    public async Task InviteParticipantShouldCreatePendingParticipantAndReturnInvitationToken()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var estates = new EstatesFake();
        estates.EstatesById[estateId] = estate;
        var invitations = new ParticipantInvitationsFake();
        var input = new InviteParticipant("  NEWUSER@example.com  ", estateId.Value(), executorId.Value());
        var flow = new InviteParticipantFlow(estates, invitations);

        var result = await flow.Execute(input);

        var participant = estate.Participants().Single();
        Assert.Equal("newuser@example.com", participant.Email());
        Assert.Equal(ParticipantStatus.Pending, participant.Status);
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.ParticipantId.Value());
        Assert.False(string.IsNullOrEmpty(result.InvitationToken.Value()));
        Assert.Single(invitations.StoredInvitations);
        Assert.Equal(result.InvitationToken.Value(), invitations.StoredInvitations.Single().Token.Value());
    }
}

public class ParticipantInvitationsFake : IParticipantInvitations
{
    public List<(InvitationToken Token, ParticipantId ParticipantId)> StoredInvitations { get; } = new();

    public Task Store(InvitationToken token, ParticipantId participantId)
    {
        StoredInvitations.Add((token, participantId));
        return Task.CompletedTask;
    }
}
