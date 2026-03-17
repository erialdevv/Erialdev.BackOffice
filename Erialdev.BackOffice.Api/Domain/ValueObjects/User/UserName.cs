using System.Text.RegularExpressions;

namespace Erialdev.BackOffice.Api.Domain.ValueObjects.User
{
    public class UserName
    {
        public string Value { get; }
        private string Pattern = @"^[a-zA-Z0-9](?:[a-zA-Z0-9._-]{2,28}[a-zA-Z0-9])$";

        public UserName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El nombre no puede estar vacío.");
            }

            if (value.Length > 30)
            {
                throw new ArgumentException("El usuario no puede exceder los 30 caracteres.");
            }

            if (!Regex.IsMatch(value, Pattern))
            {
                throw new ArgumentException("El usuario solo puede contener letras, numeros, puntos, guiones bajos y guiones.");
            }

            Value = value;
        }
    }
}