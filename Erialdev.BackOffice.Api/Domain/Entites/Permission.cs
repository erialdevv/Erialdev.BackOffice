using Erialdev.BackOffice.Api.Domain.ValueObjects;
using Erialdev.BackOffice.Api.Domain.ValueObjects.Permission;

namespace Erialdev.BackOffice.Api.Domain.Entites;

public class Permission : TenantEntity
{
    public Resource Resource { get; private set; }
    public PermissionAction Action { get; private set; }

    protected Permission() { }

    public Permission(string code, Guid companyId, Resource resource, string action, CreationAudit audit)
        : base(code, companyId, audit)
    {
        Resource = resource;
        Action = new PermissionAction(action);
    }

    private Permission(Guid id, string code, Guid companyId, Resource resource, PermissionAction action,
        DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
        bool isCanceled, DateTime? cancelDate, string? cancelAt)
        : base(id, code, companyId, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt)
    {
        Resource = resource;
        Action = action;
    }

    public static Permission Rehydrate(Guid id, string code, Guid companyId, Resource resource, string action,
        DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
        bool isCanceled, DateTime? cancelDate, string? cancelAt)
    {
        return new Permission(id, code, companyId, resource, new PermissionAction(action),
            createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt);
    }

    public void SetAction(string action, AuditActor actor)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede modificar un permiso Anulado");

        Action = new PermissionAction(action);
        UpdateEditAudit(actor);
    }
}
