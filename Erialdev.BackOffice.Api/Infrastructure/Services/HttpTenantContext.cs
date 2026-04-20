using System.Security.Claims;

namespace Erialdev.BackOffice.Api.Infrastructure.Services;

public class HttpTenantContext : ITenantContext
{
    public Guid CompanyId { get; }

    public HttpTenantContext(IHttpContextAccessor httpContextAccessor)
    {
        var companyIdClaim = httpContextAccessor.HttpContext?.User?
            .FindFirstValue("company_id");

        if (Guid.TryParse(companyIdClaim, out var companyId))
            CompanyId = companyId;
    }
}
