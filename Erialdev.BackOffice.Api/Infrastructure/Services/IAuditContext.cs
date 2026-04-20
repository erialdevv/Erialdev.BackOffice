using Erialdev.BackOffice.Api.Domain.Entites;

namespace Erialdev.BackOffice.Api.Infrastructure.Services;

public interface IAuditContext
{
    AuditActor GetActor(string? fallbackUserName = null);
    CreationAudit GetCreationAudit(string? fallbackUserName = null);
}
