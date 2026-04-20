namespace Erialdev.BackOffice.Api.Domain.Entites;

public class UserRole : TenantEntity
{
    public User User { get; private set; }
    public Role Role { get; private set; }

    protected UserRole() { }

    public UserRole(string code, Guid companyId, User user, Role role, CreationAudit audit)
        : base(code, companyId, audit)
    {
        User = user;
        Role = role;
    }

    private UserRole(Guid id, string code, Guid companyId, User user, Role role, DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
        bool isCanceled, DateTime? cancelDate, string? cancelAt)
        : base(id, code, companyId, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt)
    {
        User = user;
        Role = role;
    }

    public static UserRole Rehydrate(Guid id, string code, Guid companyId, User user, Role role, DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
        bool isCanceled, DateTime? cancelDate, string? cancelAt)
    {
        return new UserRole(id, code, companyId, user, role, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt);
    }
}
