using Erialdev.BackOffice.Api.Domain.ValueObjects;
using Erialdev.BackOffice.Api.Domain.ValueObjects.User;

namespace Erialdev.BackOffice.Api.Domain.Entites
{
    public class User:Entity
    {
        public Code Code { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public UserName UserName { get; private set; }
        public Password Password { get; private set; }
    

        public User(string code, string name, string lastname, string password, string username, string createdBy, string pcid) : base(createdBy, pcid)
        {
            Code = new Code(code);
            Name = name;
            LastName = lastname;
            UserName = new UserName(username);
            Password = new Password(password);
        }

        private User(Guid id, string code, string name, string lastname, string username, string password, 
            DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid, 
            bool isCanceled, DateTime? cancelDate, string? cancelAt) 
            : base(id, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt)
        {
            Code = new Code(code);
            Name = name;
            LastName = lastname;
            UserName = new UserName(username);
            Password = new Password(password);
        }

        public static User Rehydrate(Guid id, string code, string name, string lastname, string username, string password, 
            DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid, 
            bool isCanceled, DateTime? cancelDate, string? cancelAt)
        {
            return new User(id, code, name, lastname, username, password, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt);
        }

        public void SetName(string name, string updatedBy)
        {
            if (IsCanceled)
                throw new InvalidOperationException("No se puede modificar un usuario Anulado");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre no puede estar vacio");
            Name = name;
            UpdateEditAudit(updatedBy);
        }

        public void SetLastName(string lastname, string updatedBy)
        {
            if (IsCanceled)
                throw new InvalidOperationException("No se puede modificar un usuario Anulado");

            if (string.IsNullOrWhiteSpace(lastname))
                throw new ArgumentException("El apellido no puede estar vacio");
            LastName = lastname;
            UpdateEditAudit(updatedBy);
        }

        public void SetUserName(string username, string updatedBy)
        {
            if (IsCanceled)
                throw new InvalidOperationException("No se puede modificar un usuario Anulado");

            this.UserName = new UserName(username);
            UpdateEditAudit(updatedBy);
        }

        public void SetPassword(string password, string updatedBy)
        {
            if (IsCanceled)
                throw new InvalidOperationException("No se puede cambiar la contraseña de un usuario Anulado");

            this.Password = new Password(password);
            UpdateEditAudit(updatedBy);
        }

        public override string ToString()
        {
            return $"User: [{Code}] - {Name} {LastName} (@{UserName}) [{(IsCanceled ? "Anulado" : "Activo")}]";
        }
    }
}
