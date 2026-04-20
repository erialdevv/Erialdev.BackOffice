namespace Erialdev.BackOffice.Api.DTOs.Auth;

public sealed record JwtAccessToken(string Value, DateTime ExpiresAt);
