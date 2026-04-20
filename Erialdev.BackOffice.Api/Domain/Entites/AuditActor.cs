namespace Erialdev.BackOffice.Api.Domain.Entites;

public sealed class AuditActor
{
    public string UserName { get; }

    public AuditActor(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("El usuario de auditoria no puede estar vacio.", nameof(userName));

        UserName = userName;
    }
}
