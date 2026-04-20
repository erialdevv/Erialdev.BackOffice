namespace Erialdev.BackOffice.Api.Domain.Entites;

public sealed class CreationAudit
{
    public AuditActor Actor { get; }
    public string? Pcid { get; }

    public CreationAudit(string createdBy, string? pcid)
    {
        Actor = new AuditActor(createdBy);
        Pcid = pcid;
    }
}
