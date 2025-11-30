using EstateClear.Domain.Auth.Entities;
using EstateClear.Domain.Auth.ValueObjects;

namespace EstateClear.Application.Auth.Ports;

public interface IUsers
{
    Task<bool> ExistsByEmail(Email email);

    Task<User?> FindByEmail(Email email);

    Task Add(User user);
}
