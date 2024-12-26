using DataAccess.Models;

namespace ExtradosApi.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsersRegistered();
        List<User> GetAllActiveUsers();
        User GetUser(int id);
        User CreateUser(string name, string password, string mail, int age);
        User UpdateUser(int id, string? name = null, int? age = null, string? mail = null);
        bool DeactivateUser(int id);
    }
}
