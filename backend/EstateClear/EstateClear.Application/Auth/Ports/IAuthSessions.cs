using EstateClear.Domain.Auth.ValueObjects;

namespace EstateClear.Application.Auth.Ports;

public interface IAuthSessions
{
    Task Add(SessionToken token, UserId userId);

    Task<bool> Exists(SessionToken token);

    Task Remove(SessionToken token);
}
