namespace Erialdev.BackOffice.Api.DTOs.Auth;

public class RegisterInsertRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public Guid CompanyId { get; set; }

}
