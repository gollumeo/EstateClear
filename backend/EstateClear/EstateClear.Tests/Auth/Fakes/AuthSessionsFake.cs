using EstateClear.Application.Auth.Ports;
using EstateClear.Domain.Auth.ValueObjects;

namespace EstateClear.Tests.Auth.Fakes;

public class AuthSessionsFake : IAuthSessions
{
    private readonly HashSet<string> _tokens = new();

    public Task Add(SessionToken token, UserId userId)
    {
        _tokens.Add(token.Value());
        return Task.CompletedTask;
    }

    public Task<bool> Exists(SessionToken token)
    {
        return Task.FromResult(_tokens.Contains(token.Value()));
    }

    public Task Remove(SessionToken token)
    {
        _tokens.Remove(token.Value());
        return Task.CompletedTask;
    }
}
