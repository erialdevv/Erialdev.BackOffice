using System.Security.Claims;
using Erialdev.BackOffice.Api.Domain.Entites;

namespace Erialdev.BackOffice.Api.Infrastructure.Services;

public class HttpAuditContext : IAuditContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpAuditContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public AuditActor GetActor(string? fallbackUserName = null)
    {
        var user = _httpContextAccessor.HttpContext?.User;

        var userName = user?.FindFirstValue(ClaimTypes.Name)
            ?? user?.FindFirstValue("username")
            ?? user?.FindFirstValue("unique_name")
            ?? user?.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? user?.FindFirstValue("sub")
            ?? fallbackUserName;

        return new AuditActor(userName ?? throw new InvalidOperationException("No se pudo resolver el usuario de auditoria."));
    }

    public CreationAudit GetCreationAudit(string? fallbackUserName = null)
    {
        var actor = GetActor(fallbackUserName);
        var pcid = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

        return new CreationAudit(actor.UserName, pcid);
    }
}
