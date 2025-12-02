using EstateClear.Application.Auth.Commands;
using EstateClear.Application.Auth.Ports;
using EstateClear.Domain.Auth.ValueObjects;

namespace EstateClear.Application.Auth.Flows;

public sealed class SignOutUserFlow(IAuthSessions sessions)
{
    public async Task Execute(SignOutUser input)
    {
        var token = SessionToken.From(input.SessionToken);
        await sessions.Remove(token);
    }
}
