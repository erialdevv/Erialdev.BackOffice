using Erialdev.BackOffice.Api.Domain.ValueObjects;

namespace Erialdev.BackOffice.Api.Domain.Entites;

public class Resource : Entity
{
    public string Name { get; private set; }

    public Resource(string code, string name, string createdBy, string pcid) 
        : base(code, createdBy, pcid)
    {
        Name = name;
    }

    private Resource(Guid id, string code, string name, DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
      bool isCanceled, DateTime? cancelDate, string? cancelAt) 
        : base(id, code, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt)
    {
        Name = name;
    }

    public static Resource Rehydrate(Guid id, string code, string name, DateTime createDate, string createAt, string? editAt, DateTime? editDate, string?
    pcid, bool isCanceled, DateTime? cancelDate, string? cancelAt)
    {
        return new Resource(id, code, name, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt);
    }

    public void SetName(string name, string updatedBy)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede modificar un recurso Anulado");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre no puede estar vacio", nameof(name));

        Name = name;
        UpdateEditAudit(updatedBy);
    }

    public override string ToString()
    {
        return $"Resource: [{Code}] - {Name} [{(IsCanceled ? "Anulado" : "Activo")}]";
    }
}