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
    

        public User(string code, string name, string lastname, string password, string username, string createdBy, string pcid): base(createdBy, pcid)
        {
            Code = new Code(code);
            Name = name;
            LastName = lastname;
            UserName = new UserName(username);
            Password = new Password(password);
        }

        private User(Guid id, string username, bool isCanceled, DateTime? cancelDate) : base(id)
        {
            UserName = new UserName(username);
            IsCanceled = isCanceled;
            CancelDate = cancelDate;
        }

        public static User RehydratePartial(Guid id, string username, bool isCanceled, DateTime? cancelDate)
        {
            return new User(id, username, isCanceled, cancelDate);
        }

        public void SetName(string name ,string updatedBy)
        {
            if (IsCanceled)
                throw new InvalidOperationException("No se puede modificar un usuario Anulado");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre no puede estar vacio");
            Name = name;
            UpdateEditAudit(updatedBy);
        }

        public void SetLastName(string lastname,string updatedBy)
        {
            if (IsCanceled)
                throw new InvalidOperationException("No se puede modificar un usuario Anulado");

            if (string.IsNullOrWhiteSpace(lastname))
                throw new ArgumentException("El apellido no puede estar vacio");
            LastName = lastname;
            UpdateEditAudit(updatedBy);
        }

        public void SetUserName(string username,string updatedBy)
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

        public void Cancel(string cancelledBy)
        {
            IsCanceled = true;
            CancelDate = DateTime.Now;
            CancelAt = cancelledBy;
        }

        private void UpdateEditAudit(string editedBy)
        {
            EditDate = DateTime.Now;
            EditAt = editedBy;
        }
    }
}
