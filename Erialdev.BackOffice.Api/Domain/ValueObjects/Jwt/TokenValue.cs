namespace Erialdev.BackOffice.Api.Domain.ValueObjects.Jwt;

public class TokenValue
{
    public string Value { get; }

    public TokenValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El token no puede estar vacio.", nameof(value));

        Value = value;
    }
}