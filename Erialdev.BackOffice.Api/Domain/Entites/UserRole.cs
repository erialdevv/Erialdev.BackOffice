namespace Erialdev.BackOffice.Api.Domain.Entites;

public class UserRole : Entity
{
    public User User { get; private set; }
    public Role Role { get; private set; }

    public UserRole(string code, User user, Role role, string createdBy, string pcid)
        : base(code, createdBy, pcid)
    {
        User = user;
        Role = role;
    }

    private UserRole(Guid id, string code, User user, Role role, DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
        bool isCanceled, DateTime? cancelDate, string? cancelAt)
        : base(id, code, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt)
    {
        User = user;
        Role = role;
    }

    public static UserRole Rehydrate(Guid id, string code, User user, Role role, DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
        bool isCanceled, DateTime? cancelDate, string? cancelAt)
    {
        return new UserRole(id, code, user, role, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt);
    }

}