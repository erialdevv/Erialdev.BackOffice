using Erialdev.BackOffice.Api.Domain.Interfaces.Security;
using BC = BCrypt.Net.BCrypt;

namespace Erialdev.BackOffice.Api.Infrastructure.Security;

public class BCryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BC.HashPassword(password);
    }

    public bool Verify(string password, string hashedPassword)
    {
        return BC.Verify(password, hashedPassword);
    }
}
