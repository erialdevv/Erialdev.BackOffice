using Erialdev.BackOffice.Api.Domain.ValueObjects;

namespace Erialdev.BackOffice.Api.Domain.Entites;

public class Role : Entity
{
    public string Name { get; private set; }

    public Role(string code, string name, string createdBy, string pcid)
        : base(code, createdBy, pcid)
    {
        Name = name;
    }

    private Role(Guid id, string code, string name, DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid, bool isCanceled, DateTime? cancelDate, string? cancelAt)
        : base(id, code, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt)
    {
        Name = name;
    }

    public static Role Rehydrate(Guid id, string code, string name, DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid, bool isCanceled, DateTime? cancelDate, string? cancelAt)
    {
        return new Role(id, code, name, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt);
    }

    public void SetName(string name, string updatedBy)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede modificar un rol Anulado");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "El nombre no puede estar vacio");

        Name = name;
        UpdateEditAudit(updatedBy);
    }

    public override string ToString()
    {
        return $"Role: [{Code}] - {Name} [{(IsCanceled ? "Anulado" : "Activo")}]";
    }
}
