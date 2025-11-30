namespace EstateClear.Application.Auth.Inputs;

public sealed class SignInUser(string email, string passwordHash)
{
    public string Email { get; } = email;

    public string PasswordHash { get; } = passwordHash;
}
