using EstateClear.Application.Auth.Ports;
using EstateClear.Domain.Auth.Entities;
using EstateClear.Domain.Auth.ValueObjects;
using EstateClear.Persistence.EF.Records;
using Microsoft.EntityFrameworkCore;

namespace EstateClear.Persistence.EF.Repositories;

public sealed class UsersRepositoryEf(EstateClearDbContext context) : IUsers
{
    public async Task Add(User user)
    {
        var record = new UserRecord
        {
            Id = user.Id.Value(),
            Email = user.Email.Value(),
            PasswordHash = user.PasswordHash.Value()
        };

        context.Users.Add(record);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByEmail(Email email)
    {
        var normalizedEmail = email.Value();
        return await context.Users
            .AsNoTracking()
            .AnyAsync(user => user.Email == normalizedEmail);
    }

    public async Task<User?> FindByEmail(Email email)
    {
        var normalizedEmail = email.Value();
        var record = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == normalizedEmail);

        if (record is null)
        {
            return null;
        }

        var userId = UserId.From(record.Id);
        var userEmail = Email.From(record.Email);
        var passwordHash = PasswordHash.From(record.PasswordHash);

        return User.Create(userId, userEmail, passwordHash);
    }
}
