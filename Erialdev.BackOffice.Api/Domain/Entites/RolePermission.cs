namespace Erialdev.BackOffice.Api.Domain.Entites;

public class RolePermission : Entity
{
    public Role Role { get; private set; }
    public Permission Permission { get; private set; }

    public RolePermission(string code, Role role, Permission permission, string createdBy, string pcid)
    : base(code, createdBy, pcid)
    {
        Role = role;
        Permission = permission;
    }

    private RolePermission(Guid id, string code, Role role, Permission permission,
        DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
        bool isCanceled, DateTime? cancelDate, string? cancelAt)
        : base(id, code, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt)
    {
        Role = role;
        Permission = permission;
    }

    public static RolePermission Rehydrate(Guid id, string code, Role role, Permission permission,
        DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
        bool isCanceled, DateTime? cancelDate, string? cancelAt)
    {
        return new RolePermission(id, code, role, permission,
            createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt);
    }

}