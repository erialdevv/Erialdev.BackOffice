using Erialdev.BackOffice.Api.Domain.ValueObjects;

namespace Erialdev.BackOffice.Api.Domain.Entites;

public class Role : TenantEntity
{
    public string Name { get; private set; }

    protected Role() { }

    public Role(string code, Guid companyId, string name, CreationAudit audit)
        : base(code, companyId, audit)
    {
        Name = name;
    }

    private Role(Guid id, string code, Guid companyId, string name, DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid, bool isCanceled, DateTime? cancelDate, string? cancelAt)
        : base(id, code, companyId, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt)
    {
        Name = name;
    }

    public static Role Rehydrate(Guid id, string code, Guid companyId, string name, DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid, bool isCanceled, DateTime? cancelDate, string? cancelAt)
    {
        return new Role(id, code, companyId, name, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt);
    }

    public void SetName(string name, AuditActor actor)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede modificar un rol Anulado");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "El nombre no puede estar vacio");

        Name = name;
        UpdateEditAudit(actor);
    }

    public override string ToString() =>
        $"Role: [{Code}] - {Name} [{(IsCanceled ? "Anulado" : "Activo")}]";
}
