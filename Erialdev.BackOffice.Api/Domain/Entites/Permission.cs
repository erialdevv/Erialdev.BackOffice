using Erialdev.BackOffice.Api.Domain.ValueObjects;
using Erialdev.BackOffice.Api.Domain.ValueObjects.Permission;

namespace Erialdev.BackOffice.Api.Domain.Entites;

public class Permission : Entity
{
    public Resource Resource { get; private set; }
    public PermissionAction Action { get; private set; }

    public Permission(string code, Resource resource, string action, string createdBy, string pcid)
        : base(code, createdBy, pcid)
    {
        Resource = resource;
        Action = new PermissionAction(action);
    }

    private Permission(Guid id, string code, Resource resource, PermissionAction action,
        DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
        bool isCanceled, DateTime? cancelDate, string? cancelAt)
        : base(id, code, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt)
    {
        Resource = resource;
        Action = action;
    }

    public static Permission Rehydrate(Guid id, string code, Resource resource, string action,
        DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
        bool isCanceled, DateTime? cancelDate, string? cancelAt)
    {
        return new Permission(id, code, resource, new PermissionAction(action),
            createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt);
    }

    public void SetAction(string action, string updatedBy)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede modificar un permiso Anulado");

        Action = new PermissionAction(action);
        UpdateEditAudit(updatedBy);
    }
}