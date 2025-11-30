using EstateClear.Application.Auth.Ports;
using EstateClear.Domain.Auth.Entities;
using EstateClear.Domain.Auth.ValueObjects;

namespace EstateClear.Tests.Auth.Fakes;

public class UsersFake : IUsers
{
    public Dictionary<Email, (UserId UserId, PasswordHash PasswordHash)> ExistingUsers { get; } = new();

    public List<(UserId UserId, Email Email, PasswordHash PasswordHash)> AddedUsers { get; } = [];

    public Task Add(User user)
    {
        AddedUsers.Add((user.Id, user.Email, user.PasswordHash));
        ExistingUsers[user.Email] = (user.Id, user.PasswordHash);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsByEmail(Email email)
    {
        var existsInExisting = ExistingUsers.ContainsKey(email);
        var existsInAdded = AddedUsers.Any(user => user.Email == email);
        return Task.FromResult(existsInExisting || existsInAdded);
    }

    public Task<User?> FindByEmail(Email email)
    {
        if (ExistingUsers.TryGetValue(email, out var existing))
        {
            var user = User.Create(existing.UserId, email, existing.PasswordHash);
            return Task.FromResult<User?>(user);
        }

        var added = AddedUsers.FirstOrDefault(user => user.Email == email);

        if (added.Email is not null)
        {
            var user = User.Create(added.UserId, added.Email, added.PasswordHash);
            return Task.FromResult<User?>(user);
        }

        return Task.FromResult<User?>(null);
    }
}
