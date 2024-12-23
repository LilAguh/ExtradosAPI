using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IUserDAO
    {
        List<User> GetAllUsersRegistered();
        List<User> GetAllActiveUsers();
        User GetUser(int id);
        User CreateUser(string name, string mail, int age); // Actualizar firma aquí
        User UpdateUser(int id, string? name = null, int? age = null, string? mail = null);
        bool DesactivateUser(int id);
    }
}
