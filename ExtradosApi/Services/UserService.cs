using DataAccess.Interfaces;
using DataAccess.Implementations;
using DataAccess.Models;
using ExtradosApi.Services.Interfaces;

namespace ExtradosApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDAO _userDAO;

        public UserService(IUserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public List<User> GetAllUsersRegistered()
        {
            return _userDAO.GetAllUsersRegistered();
        }

        public List<User> GetAllActiveUsers()
        {
            return _userDAO.GetAllActiveUsers();
        }
        public User GetUserById(int id)
        {
            return _userDAO.GetUserById(id);
        }
        public User GetUser(string mail, string password)
        {
            return _userDAO.GetUser(mail, password);
        }

        public User CreateUser(string name, string password, string mail, int age)
        {
            if (age <= 14)
                throw new ArgumentException("Age must be greater than 14.");
            if (!mail.Contains("@gmail.com"))
                throw new ArgumentException("Email must be a Gmail address.");

            return _userDAO.CreateUser(name, password, mail, age);
        }

        public User UpdateUser(int id, string? name = null, int? age = null, string? mail = null)
        {
            if (age.HasValue && age <= 14)
                throw new ArgumentException("Age must be greater than 14.");
            if (!string.IsNullOrEmpty(mail) && !mail.Contains("@gmail.com"))
                throw new ArgumentException("Email must be a Gmail address.");

            return _userDAO.UpdateUser(id, name, age, mail);
        }

        public bool DeactivateUser(int id)
        {
            return _userDAO.DesactivateUser(id);
        }
    }
}
