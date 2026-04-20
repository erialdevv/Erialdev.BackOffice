using Erialdev.BackOffice.Api.Domain.ValueObjects;

namespace Erialdev.BackOffice.Api.Domain.Entites;

public class Resource : TenantEntity
{
    public string Name { get; private set; }

    protected Resource() { }

    public Resource(string code, Guid companyId, string name, CreationAudit audit)
        : base(code, companyId, audit)
    {
        Name = name;
    }

    private Resource(Guid id, string code, Guid companyId, string name, DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
        bool isCanceled, DateTime? cancelDate, string? cancelAt)
        : base(id, code, companyId, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt)
    {
        Name = name;
    }

    public static Resource Rehydrate(Guid id, string code, Guid companyId, string name, DateTime createDate, string createAt, string? editAt, DateTime? editDate, string?
    pcid, bool isCanceled, DateTime? cancelDate, string? cancelAt)
    {
        return new Resource(id, code, companyId, name, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt);
    }

    public void SetName(string name, AuditActor actor)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede modificar un recurso Anulado");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre no puede estar vacio", nameof(name));

        Name = name;
        UpdateEditAudit(actor);
    }

    public override string ToString() =>
        $"Resource: [{Code}] - {Name} [{(IsCanceled ? "Anulado" : "Activo")}]";
}
