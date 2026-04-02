namespace Erialdev.BackOffice.Api.Domain.ValueObjects.Permission;

public class PermissionAction
{
    public string Value { get; }
    public static readonly string Create = "CREATE";
    public static readonly string Read = "READ";
    public static readonly string Update = "UPDATE";
    public static readonly string Delete = "DELETE";

    private static readonly List<string> ActionsOk = new() { Create, Read, Update, Delete };

    public PermissionAction(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException("La acción no puede estar vacia");
        
        var valueUpper = value.ToUpper();
        if (!ActionsOk.Contains(valueUpper))
            throw new ArgumentException($"La acción '{value}' no es válida.");
        
        Value = valueUpper;
    }
}