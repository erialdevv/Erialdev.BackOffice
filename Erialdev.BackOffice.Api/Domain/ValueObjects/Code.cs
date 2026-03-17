
using System.Text.RegularExpressions;


namespace Erialdev.BackOffice.Api.Domain.ValueObjects
{
    public class Code
    {
        public string Value { get; }
        private string Pattern = @"^[A-Z]{6}-\d{4}-\d{10}$";


        public Code(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("El código no puede estar vacio");

            if (!Regex.IsMatch(value, Pattern))
                throw new ArgumentException("Formato de código inválido");

            Value = value;

        }

    }
}
