using Erialdev.BackOffice.Api.DTOs.Auth;
using Erialdev.BackOffice.Api.Domain.Entites;

namespace Erialdev.BackOffice.Api.Domain.Interfaces.Security;

public interface IJwtTokenService
{
    JwtAccessToken CreateAccessToken(User user);
    string CreateRefreshToken();
}
