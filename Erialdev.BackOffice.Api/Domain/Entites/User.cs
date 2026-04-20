using Erialdev.BackOffice.Api.Domain.ValueObjects;
using Erialdev.BackOffice.Api.Domain.ValueObjects.Jwt;
using Erialdev.BackOffice.Api.Domain.ValueObjects.User;

namespace Erialdev.BackOffice.Api.Domain.Entites;

public class User : TenantEntity
{
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public UserName UserName { get; private set; }
    public Password Password { get; private set; }

    public List<UserRole> UserRoles { get; private set; } = [];
    public List<RefreshToken> RefreshTokens { get; private set; } = [];

    protected User() { }

    public User(string code, Guid companyId, string name, string lastname, string password, string username, CreationAudit audit)
        : base(code, companyId, audit)
    {
        Name = name;
        LastName = lastname;
        UserName = new UserName(username);
        Password = new Password(password);
    }

    private User(Guid id, string code, Guid companyId, string name, string lastname, string username, string password,
        DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
        bool isCanceled, DateTime? cancelDate, string? cancelAt)
        : base(id, code, companyId, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt)
    {
        Name = name;
        LastName = lastname;
        UserName = new UserName(username);
        Password = new Password(password);
    }

    public static User Rehydrate(Guid id, string code, Guid companyId, string name, string lastname, string username, string password,
        DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
        bool isCanceled, DateTime? cancelDate, string? cancelAt)
    {
        return new User(id, code, companyId, name, lastname, username, password, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt);
    }

    public void SetName(string name, AuditActor actor)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede modificar un usuario Anulado");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre no puede estar vacio", nameof(name));

        Name = name;
        UpdateEditAudit(actor);
    }

    public void SetLastName(string lastname, AuditActor actor)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede modificar un usuario Anulado");

        if (string.IsNullOrWhiteSpace(lastname))
            throw new ArgumentException("El apellido no puede estar vacio", nameof(lastname));

        LastName = lastname;
        UpdateEditAudit(actor);
    }

    public void SetUserName(string username, AuditActor actor)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede modificar un usuario Anulado");

        UserName = new UserName(username);
        UpdateEditAudit(actor);
    }

    public void SetPassword(string password, AuditActor actor)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede cambiar la contrasena de un usuario Anulado");

        Password = new Password(password);
        UpdateEditAudit(actor);
    }

    public void AddRole(Role role, string createdBy, string pcid)
    {
        var audit = new CreationAudit(createdBy, pcid);
        var assignment = new UserRole(Guid.CreateVersion7().ToString(), CompanyId, this, role, audit);
        UserRoles.Add(assignment);
        UpdateEditAudit(audit.Actor);
    }

    public void RemoveRole(Role role, AuditActor actor)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede cambiar el rol de un usuario Anulado");

        var assignment = UserRoles.FirstOrDefault(x => x.Role.Id == role.Id);
        if (assignment != null)
        {
            UserRoles.Remove(assignment);
            UpdateEditAudit(actor);
        }
    }

    public override string ToString() =>
        $"User: [{Code}] - {Name} {LastName} (@{UserName}) [{(IsCanceled ? "Anulado" : "Activo")}]";

    public RefreshToken CreateRefreshToken(TokenValue newToken, DateTime expiresAt, string createdBy, string? pcid = null)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede emitir un refresh token a un usuario Anulado");

        var refreshToken = RefreshToken.Create(newToken, expiresAt, Id, CompanyId, createdBy, pcid);
        RefreshTokens.Add(refreshToken);
        UpdateEditAudit(new AuditActor(createdBy));

        return refreshToken;
    }

    public RefreshToken? GetActiveRefreshToken(string token) =>
        RefreshTokens.FirstOrDefault(x => x.Token.Value == token && x.IsActive());

    public void RevokeRefreshToken(string token, AuditActor actor)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede revocar un refresh token de un usuario Anulado");

        var refreshToken = RefreshTokens.FirstOrDefault(x => x.Token.Value == token);
        if (refreshToken is null)
            return;

        refreshToken.Revoke();
        UpdateEditAudit(actor);
    }

    public void RevokeAllRefreshTokens(AuditActor actor)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede revocar refresh tokens de un usuario Anulado");

        foreach (var refreshToken in RefreshTokens.Where(x => x.IsActive()))
            refreshToken.Revoke();

        UpdateEditAudit(actor);
    }
}
