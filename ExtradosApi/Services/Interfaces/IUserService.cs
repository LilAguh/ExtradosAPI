using DataAccess.Models;

namespace ExtradosApi.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsersRegistered();
        List<User> GetAllActiveUsers();
        User GetUser(string mail, string password);
        User CreateUser(string name, string password, string mail, int age);
        User GetUserById(int id);
        User UpdateUser(int id, string? name = null, int? age = null, string? mail = null, string? password = null);
        bool DeactivateUser(int id);
    }
}
