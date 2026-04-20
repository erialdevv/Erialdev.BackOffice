using Erialdev.BackOffice.Api.Domain.ValueObjects.Jwt;

namespace Erialdev.BackOffice.Api.Domain.Entites;

public class RefreshToken
{
    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public TokenValue Token { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedBy { get; private set; }
    public string? Pcid { get; private set; }
    public bool IsRevoked { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public Guid UserId { get; private set; }

    protected RefreshToken() { }

    private RefreshToken(Guid id, TokenValue token, DateTime expiresAt, DateTime createdAt, Guid userId, Guid companyId, string createdBy, string? pcid)
    {
        Id = id;
        Token = token;
        ExpiresAt = expiresAt;
        CreatedAt = createdAt;
        UserId = userId;
        CompanyId = companyId;
        CreatedBy = createdBy;
        Pcid = pcid;
        IsRevoked = false;
    }

    public static RefreshToken Create(TokenValue token, DateTime expiresAt, Guid userId, Guid companyId, string createdBy, string? pcid = null)
    {
        if (expiresAt <= DateTime.UtcNow)
            throw new ArgumentException("La fecha de expiracion no puede ser menor a la fecha actual", nameof(expiresAt));

        if (userId == Guid.Empty)
            throw new ArgumentException("El usuario no puede ser vacio", nameof(userId));

        if (companyId == Guid.Empty)
            throw new ArgumentException("La empresa no puede ser vacia", nameof(companyId));

        if (string.IsNullOrWhiteSpace(createdBy))
            throw new ArgumentException("El creador no puede estar vacio", nameof(createdBy));

        return new RefreshToken(Guid.CreateVersion7(), token, expiresAt, DateTime.UtcNow, userId, companyId, createdBy, pcid);
    }

    public bool IsExpired() => DateTime.UtcNow >= ExpiresAt;

    public bool IsActive() => !IsRevoked && !IsExpired();

    public void Revoke()
    {
        if (IsRevoked)
            return;

        IsRevoked = true;
        RevokedAt = DateTime.UtcNow;
    }
}
