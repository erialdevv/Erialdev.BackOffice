using System.Text.RegularExpressions;

namespace Erialdev.BackOffice.Api.Domain.ValueObjects.User{
    public class Password{

        public string Value{get;}

        private string Pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

        public Password(string value){
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "La contraseña no puede estar vacia");

            if(!Regex.IsMatch(value,Pattern))
                throw new ArgumentException("La contraseña debe tener al menos 8 caracteres, una mayuscula, una minuscula, un numero y un caracter especial", nameof(value));

            Value=value;
        }

    }
}