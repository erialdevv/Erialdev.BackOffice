namespace Erialdev.BackOffice.Api.Domain.Entites;

public class RolePermission : TenantEntity
{
    public Role Role { get; private set; }
    public Permission Permission { get; private set; }

    protected RolePermission() { }

    public RolePermission(string code, Guid companyId, Role role, Permission permission, CreationAudit audit)
        : base(code, companyId, audit)
    {
        Role = role;
        Permission = permission;
    }

    private RolePermission(Guid id, string code, Guid companyId, Role role, Permission permission,
        DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
        bool isCanceled, DateTime? cancelDate, string? cancelAt)
        : base(id, code, companyId, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt)
    {
        Role = role;
        Permission = permission;
    }

    public static RolePermission Rehydrate(Guid id, string code, Guid companyId, Role role, Permission permission,
        DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
        bool isCanceled, DateTime? cancelDate, string? cancelAt)
    {
        return new RolePermission(id, code, companyId, role, permission,
            createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt);
    }
}
