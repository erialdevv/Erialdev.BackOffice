namespace Erialdev.BackOffice.Api.DTOs.Auth;

public sealed record AuthSession(
    Guid UserId,
    Guid CompanyId,
    string UserName,
    string AccessToken,
    DateTime AccessTokenExpiresAt,
    string RefreshToken,
    DateTime RefreshTokenExpiresAt);
