namespace Erialdev.BackOffice.Api.DTOs.Auth;

public sealed record AuthenticateRequest(Guid CompanyId, string Username, string Password);
