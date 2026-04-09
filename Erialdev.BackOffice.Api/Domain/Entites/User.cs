using System.Linq;
using Erialdev.BackOffice.Api.Domain.ValueObjects;
using Erialdev.BackOffice.Api.Domain.ValueObjects.User;

namespace Erialdev.BackOffice.Api.Domain.Entites;

public class User : Entity
{
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public UserName UserName { get; private set; }
    public Password Password { get; private set; }

    public List<UserRole> UserRoles { get; private set; } = [];

    public User(string code, string name, string lastname, string password, string username, string createdBy, string pcid)
        : base(code, createdBy, pcid)
    {
        Name = name;
        LastName = lastname;
        UserName = new UserName(username);
        Password = new Password(password);
    }

    private User(Guid id, string code, string name, string lastname, string username, string password,
        DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid,
        bool isCanceled, DateTime? cancelDate, string? cancelAt)
        : base(id, code, createDate, createAt, editAt, editDate, pcid, isCanceled, cancelDate, cancelAt)
    {
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
            throw new ArgumentException("El nombre no puede estar vacio", nameof(name));

        Name = name;
        UpdateEditAudit(updatedBy);
    }

    public void SetLastName(string lastname, string updatedBy)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede modificar un usuario Anulado");

        if (string.IsNullOrWhiteSpace(lastname))
            throw new ArgumentException("El apellido no puede estar vacio", nameof(lastname));

        LastName = lastname;
        UpdateEditAudit(updatedBy);
    }

    public void SetUserName(string username, string updatedBy)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede modificar un usuario Anulado");

        UserName = new UserName(username);
        UpdateEditAudit(updatedBy);
    }

    public void SetPassword(string password, string updatedBy)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede cambiar la contraseña de un usuario Anulado");

        Password = new Password(password);
        UpdateEditAudit(updatedBy);
    }

    public void AddRole(Role role, string createdBy, string pcid)
    {
        var assignment = new UserRole(Guid.NewGuid().ToString(), this, role, createdBy, pcid);
        UserRoles.Add(assignment);
        UpdateEditAudit(createdBy);
    }

    public void RemoveRole(Role role, string updatedBy)
    {
        if (IsCanceled)
            throw new InvalidOperationException("No se puede cambiar el rol  de un usuario Anulado");

        var assignment = UserRoles.FirstOrDefault(x => x.Role.Id == role.Id);
        if (assignment != null)
        {
            UserRoles.Remove(assignment);
            UpdateEditAudit(updatedBy);
        }

    }


    public override string ToString()
    {
        return $"User: [{Code}] - {Name} {LastName} (@{UserName}) [{(IsCanceled ? "Anulado" : "Activo")}]";
    }
}
