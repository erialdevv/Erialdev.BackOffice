using Erialdev.BackOffice.Api.Domain.ValueObjects;

namespace Erialdev.BackOffice.Api.Domain.Entites;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public Code Code { get; protected set; }

    public DateTime CreateDate { get; protected set; }
    public string CreateAt { get; protected set; }
    public DateTime? EditDate { get; protected set; }
    public string? EditAt { get; protected set; }
    public DateTime? CancelDate { get; protected set; }
    public string? CancelAt { get; protected set; }
    public bool IsCanceled { get; protected set; }
    public string? Pcid { get; protected set; }

    protected Entity() { }

    protected Entity(string code, CreationAudit audit)
    {
        Id = Guid.CreateVersion7();
        Code = new Code(code);
        CreateDate = DateTime.UtcNow;
        CreateAt = audit.Actor.UserName;
        Pcid = audit.Pcid;
    }

    protected Entity(Guid id, string code, DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid, bool isCanceled, DateTime? cancelDate, string? cancelAt)
    {
        Id = id;
        Code = new Code(code);
        CreateDate = createDate;
        CreateAt = createAt;
        EditAt = editAt;
        EditDate = editDate;
        Pcid = pcid;
        IsCanceled = isCanceled;
        CancelDate = cancelDate;
        CancelAt = cancelAt;
    }

    public virtual void Cancel(AuditActor actor)
    {
        if (IsCanceled) return;
        IsCanceled = true;
        CancelDate = DateTime.UtcNow;
        CancelAt = actor.UserName;
    }

    protected void UpdateEditAudit(AuditActor actor)
    {
        EditDate = DateTime.UtcNow;
        EditAt = actor.UserName;
    }
}
