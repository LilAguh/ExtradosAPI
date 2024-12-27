using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IUserDAO
    {
        List<User> GetAllUsersRegistered();
        List<User> GetAllActiveUsers();
        User GetUser(string mail, string password);
        User CreateUser(string name,string password, string mail, int age); // Actualizar firma aquí
        User GetUserById(int id);
        User UpdateUser(int id, string name, int age, string mail, string password);
        bool DesactivateUser(int id);
    }
}
