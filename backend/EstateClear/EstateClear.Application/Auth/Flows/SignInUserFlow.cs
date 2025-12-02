using EstateClear.Application.Auth.Commands;
using EstateClear.Application.Auth.Ports;
using EstateClear.Domain.Auth.ValueObjects;

namespace EstateClear.Application.Auth.Flows;

public sealed class SignInUserFlow(IUsers users)
{
    public async Task<SignedInUser> Execute(SignInUser input)
    {
        var email = Email.From(input.Email);
        var user = await users.FindByEmail(email);

        if (user is null)
        {
            throw new Exception("User not found");
        }

        var passwordHash = PasswordHash.From(input.PasswordHash);

        if (user.PasswordHash.Value() != passwordHash.Value())
        {
            throw new Exception("Invalid credentials");
        }

        var sessionToken = SessionToken.From(Guid.NewGuid().ToString());

        return new SignedInUser(user.Id, sessionToken);
    }
}

public sealed class SignedInUser(UserId userId, SessionToken sessionToken)
{
    public UserId UserId { get; } = userId;

    public SessionToken SessionToken { get; } = sessionToken;
}
