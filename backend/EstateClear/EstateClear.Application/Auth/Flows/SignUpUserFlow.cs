using EstateClear.Application.Auth.Inputs;
using EstateClear.Application.Auth.Ports;
using EstateClear.Domain.Auth.Entities;
using EstateClear.Domain.Auth.ValueObjects;

namespace EstateClear.Application.Auth.Flows;

public sealed class SignUpUserFlow(IUsers users)
{
    public async Task<SignedUpUser> Execute(SignUpUser input)
    {
        var email = Email.From(input.Email);
        var exists = await users.ExistsByEmail(email);

        if (exists)
        {
            throw new Exception("Email already exists");
        }

        var passwordHash = PasswordHash.From(input.PasswordHash);
        var userId = UserId.From(Guid.NewGuid());
        var user = User.Create(userId, email, passwordHash);

        await users.Add(user);

        return new SignedUpUser(userId);
    }
}

public sealed class SignedUpUser(UserId userId)
{
    public UserId UserId { get; } = userId;
}
