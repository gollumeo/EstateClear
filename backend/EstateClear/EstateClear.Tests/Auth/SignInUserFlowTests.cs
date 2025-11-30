using EstateClear.Application.Auth.Flows;
using EstateClear.Application.Auth.Inputs;
using EstateClear.Domain.Auth.ValueObjects;
using EstateClear.Tests.Auth.Fakes;

namespace EstateClear.Tests.Auth;

public class SignInUserFlowTests
{
    [Fact]
    public async Task SigningInWithValidCredentialsReturnsUserId()
    {
        var email = Email.From("user@example.com");
        var passwordHash = PasswordHash.From("hashed-password");
        var users = new UsersFake();
        var userId = UserId.From(Guid.NewGuid());
        users.ExistingUsers[email] = (userId, passwordHash);
        var input = new SignInUser("  USER@example.com  ", "hashed-password");
        var flow = new SignInUserFlow(users);

        var result = await flow.Execute(input);

        Assert.NotNull(result);
        Assert.Equal(userId.Value(), result.UserId.Value());
    }

    [Fact]
    public async Task SigningInWithUnknownEmailShouldThrow()
    {
        var input = new SignInUser("missing@example.com", "hashed-password");
        var users = new UsersFake();
        var flow = new SignInUserFlow(users);

        var action = () => flow.Execute(input);

        await Assert.ThrowsAnyAsync<Exception>(action);
    }

    [Fact]
    public async Task SigningInWithWrongPasswordShouldThrow()
    {
        var email = Email.From("user@example.com");
        var users = new UsersFake();
        var userId = UserId.From(Guid.NewGuid());
        users.ExistingUsers[email] = (userId, PasswordHash.From("expected-hash"));
        var input = new SignInUser("user@example.com", "wrong-hash");
        var flow = new SignInUserFlow(users);

        var action = () => flow.Execute(input);

        await Assert.ThrowsAnyAsync<Exception>(action);
    }
}
