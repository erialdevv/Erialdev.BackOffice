using Erialdev.BackOffice.Api.Infrastructure.Persistence;
using Erialdev.BackOffice.Api.Infrastructure.Services;
using Erialdev.BackOffice.Api.Domain.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Erialdev.BackOffice.Api.Controllers.Record;

using Erialdev.BackOffice.Api.DTOs.Record;

[ApiController]
[Route("api/new-company")]
public class CompanyController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IAuditContext _auditContext;

    public CompanyController(ApplicationDbContext context, IAuditContext auditContext)
    {
        _context = context;
        _auditContext = auditContext;
    }

    [HttpPost]
    public async Task<IActionResult> InsertCompany([FromBody] CompanyInsertRequest request)
    {
        var existsCompany = await _context.Companies.IgnoreQueryFilters()
        .AnyAsync(x => EF.Property<string>(x, nameof(Company.Code)) == request.Code);

        if (existsCompany)
            return BadRequest("La empresa ya existe");

        try
        {
            var audit = _auditContext.GetCreationAudit(request.Code);

            var newCompany = new Company(
                request.Code,
                request.Name,
                audit
            );

            _context.Companies.Add(newCompany);
            await _context.SaveChangesAsync();

            return Ok("Empresa creada correctamente");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }



}

