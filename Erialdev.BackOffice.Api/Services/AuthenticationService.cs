using Erialdev.BackOffice.Api.DTOs.Auth;
using Erialdev.BackOffice.Api.Domain.Entites;
using Erialdev.BackOffice.Api.Domain.Interfaces.Security;
using Erialdev.BackOffice.Api.Domain.ValueObjects.Jwt;
using Erialdev.BackOffice.Api.Infrastructure.Persistence;
using Erialdev.BackOffice.Api.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Erialdev.BackOffice.Api.Services;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly JwtSettings _jwtSettings;

    public AuthenticationService(
        ApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthSession> AuthenticateAsync(Guid companyId, string username, string password, string? pcid = null, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users
            .IgnoreQueryFilters()
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.CompanyId == companyId && EF.Property<string>(x, nameof(User.UserName)) == username, cancellationToken);

        if (user is null || user.IsCanceled)
            throw new UnauthorizedAccessException("Credenciales invalidas.");

        if (!_passwordHasher.Verify(password, user.Password.Value))
            throw new UnauthorizedAccessException("Credenciales invalidas.");

        var accessToken = _jwtTokenService.CreateAccessToken(user);
        var refreshTokenValue = _jwtTokenService.CreateRefreshToken();
        var refreshExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);

        user.CreateRefreshToken(new TokenValue(refreshTokenValue), refreshExpiresAt, user.UserName.Value, pcid);
        await _context.SaveChangesAsync(cancellationToken);

        return new AuthSession(
            user.Id,
            user.CompanyId,
            user.UserName.Value,
            accessToken.Value,
            accessToken.ExpiresAt,
            refreshTokenValue,
            refreshExpiresAt);
    }

    public async Task<AuthSession> RefreshAsync(Guid companyId, string refreshToken, string? pcid = null, CancellationToken cancellationToken = default)
    {
        var currentRefreshToken = await _context.RefreshTokens
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.CompanyId == companyId && EF.Property<string>(x, nameof(RefreshToken.Token)) == refreshToken, cancellationToken);

        if (currentRefreshToken is null || !currentRefreshToken.IsActive())
            throw new UnauthorizedAccessException("Refresh token invalido.");

        var user = await _context.Users
            .IgnoreQueryFilters()
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == currentRefreshToken.UserId && x.CompanyId == companyId, cancellationToken);

        if (user is null || user.IsCanceled)
            throw new UnauthorizedAccessException("Refresh token invalido.");

        currentRefreshToken.Revoke();

        var accessToken = _jwtTokenService.CreateAccessToken(user);
        var newRefreshTokenValue = _jwtTokenService.CreateRefreshToken();
        var refreshExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);

        user.CreateRefreshToken(new TokenValue(newRefreshTokenValue), refreshExpiresAt, user.UserName.Value, pcid);
        await _context.SaveChangesAsync(cancellationToken);

        return new AuthSession(
            user.Id,
            user.CompanyId,
            user.UserName.Value,
            accessToken.Value,
            accessToken.ExpiresAt,
            newRefreshTokenValue,
            refreshExpiresAt);
    }

    public async Task RevokeAsync(Guid companyId, string refreshToken, CancellationToken cancellationToken = default)
    {
        var currentRefreshToken = await _context.RefreshTokens
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.CompanyId == companyId && EF.Property<string>(x, nameof(RefreshToken.Token)) == refreshToken, cancellationToken);

        if (currentRefreshToken is null)
            return;

        currentRefreshToken.Revoke();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
