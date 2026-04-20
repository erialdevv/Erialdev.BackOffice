using Erialdev.BackOffice.Api.DTOs.Auth;

namespace Erialdev.BackOffice.Api.Domain.Interfaces.Security;

public interface IAuthenticationService
{
    Task<AuthSession> AuthenticateAsync(Guid companyId, string username, string password, string? pcid = null, CancellationToken cancellationToken = default);
    Task<AuthSession> RefreshAsync(Guid companyId, string refreshToken, string? pcid = null, CancellationToken cancellationToken = default);
    Task RevokeAsync(Guid companyId, string refreshToken, CancellationToken cancellationToken = default);
}
