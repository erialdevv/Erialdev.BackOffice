using Erialdev.BackOffice.Api.Domain.ValueObjects;

namespace Erialdev.BackOffice.Api.Domain.Entites;

public abstract class TenantEntity : Entity
{
    public Guid CompanyId { get; protected set; }

    protected TenantEntity() { }

    protected TenantEntity(string code, Guid companyId, CreationAudit audit)
        : base(code, audit)
    {
        CompanyId = companyId;
    }

    protected TenantEntity(Guid id, string code, Guid companyId, DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid, bool isCanceled, DateTime? cancelDate, string? cancelAt)
        : base(id, code, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt)
    {
        CompanyId = companyId;
    }
}
