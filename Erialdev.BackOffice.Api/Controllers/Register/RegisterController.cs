using Erialdev.BackOffice.Api.Infrastructure.Persistence;
using Erialdev.BackOffice.Api.Infrastructure.Services;
using Erialdev.BackOffice.Api.Domain.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Erialdev.BackOffice.Api.Controllers.Register;

using Erialdev.BackOffice.Api.DTOs.Auth;
using Erialdev.BackOffice.Api.Domain.Interfaces.Security;

[ApiController]
[Route("api/register")]
public class RegisterController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuditContext _auditContext;

    public RegisterController(ApplicationDbContext context, IPasswordHasher passwordHasher, IAuditContext auditContext)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _auditContext = auditContext;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterInsertRequest request)
    {
        var existsUser = await _context.Users.IgnoreQueryFilters().AnyAsync(x =>
            x.CompanyId == request.CompanyId &&
            EF.Property<string>(x, "UserName") == request.Username);


        if (existsUser)
            return BadRequest("El usuario ya fue creado");

        Domain.ValueObjects.User.Password.Validate(request.Password);

        string hashedPassword = _passwordHasher.Hash(request.Password);

        try
        {
            var audit = _auditContext.GetCreationAudit(request.Username);

            var newuser = new User(
            request.Code,
            request.CompanyId,
            request.Name,
            request.LastName,
            hashedPassword,
            request.Username,
            audit
        );

            _context.Users.Add(newuser);
            await _context.SaveChangesAsync();

            return Ok("Usuario Registrado correctamente");

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    private string nameof(object userName)
    {
        throw new NotImplementedException();
    }

}
