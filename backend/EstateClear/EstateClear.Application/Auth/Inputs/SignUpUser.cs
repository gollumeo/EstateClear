namespace EstateClear.Application.Auth.Inputs;

public sealed class SignUpUser(string email, string passwordHash)
{
    public string Email { get; } = email;

    public string PasswordHash { get; } = passwordHash;
}
