namespace Erialdev.BackOffice.Api.Domain.Entites;

public class Company : Entity
{
    public string Name { get; private set; }

    protected Company() { }

    public Company(string code, string name, CreationAudit audit)
        : base(code, audit)
    {
        Name = name;
    }

    private Company(Guid id, string code, string name, DateTime createDate, string createAt, string? editAt,
        DateTime? editDate, string? pcid, bool isCanceled, DateTime? cancelDate, string? cancelAt)
        : base(id, code, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt)
    {
        Name = name;
    }

    public static Company Rehydrate(Guid id, string code, string name, DateTime createDate, string createAt,
        string? editAt, DateTime? editDate, string? pcid, bool isCanceled, DateTime? cancelDate, string? cancelAt)
    {
        return new Company(id, code, name, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt);
    }

    public void SetName(string name, AuditActor actor)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede modificar una empresa anulada");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre de la empresa no puede estar vacio.", nameof(name));

        Name = name;
        UpdateEditAudit(actor);
    }

    public override string ToString() => $"Company: [{Code}] - {Name} [{(IsCanceled ? "Anulada" : "Activa")}]";
}
