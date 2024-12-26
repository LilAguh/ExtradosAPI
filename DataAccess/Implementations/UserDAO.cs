using DataAccess.Interfaces;
using DataAccess.Models;
using MySqlConnector;
using Dapper;
using BCrypt.Net;

namespace DataAccess.Implementations
{
    public class UserDAO : IUserDAO
    {
        private string connectionString = "Server=127.0.0.1;Database=ApiEjercicio;UId=aguh;Password=asd123;";

        public List<User> GetAllUsersRegistered()
        {
            string query = "SELECT * FROM Users;";
            using (var connection = new MySqlConnection(connectionString))
            {
                return connection.Query<User>(query).AsList();
            }
        }

        public List<User> GetAllActiveUsers()
        {
            string query = "SELECT * FROM Users WHERE active = 1;";
            using (var connection = new MySqlConnection(connectionString))
            {
                return connection.Query<User>(query).AsList();
            }
        }

        public User CreateUser(string name,string password, string mail, int age)
        {
            string passHash = BCrypt.Net.BCrypt.HashPassword(password);
            string query = "INSERT INTO Users (name, mail, age, password) VALUES (@name, @mail, @age, @passHash);";
            string queryUserCreated = "SELECT * FROM Users WHERE id = LAST_INSERT_ID();";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(query, new { name, passHash, mail, age });
                var user = connection.QueryFirstOrDefault<User>(queryUserCreated);
                return user;
            }
        }

        public User UpdateUser(int id, string? name = null, int? age = null, string? mail = null)
        {
            string queryUpdateUser = "SELECT * FROM Users WHERE id = @id;";
            string query = "UPDATE Users SET name = @name, age = @age, mail = @mail WHERE id = @id;";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var user = connection.QueryFirstOrDefault<User>(queryUpdateUser, new { id });
                if (user == null)
                    throw new Exception($"User with ID {id} not found.");

                string newName = name ?? user.Name;
                int newAge = age ?? user.Age;
                string newMail = mail ?? user.Mail;

                connection.Execute(query, new { name = newName, age = newAge, mail = newMail, id });
                return connection.QueryFirstOrDefault<User>(queryUpdateUser, new { id });
            }
        }

        public bool DesactivateUser(int id)
        {
            string query = "UPDATE Users SET active = 0 WHERE id = @id;";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return connection.Execute(query, new { id }) > 0;
            }
        }

        public User GetUserById(int id)
        {
            string query = "SELECT * FROM Users WHERE id = @id;";
            using (var connection = new MySqlConnection(connectionString))
            {
                // Ejecutar la consulta y devolver el primer resultado
                return connection.QueryFirstOrDefault<User>(query, new { id });
            }
        }

        public User GetUser(string mail, string password)
        {
            string query = "SELECT * FROM Users WHERE mail = @mail;";
            using (var connection = new MySqlConnection(connectionString))
            {
                var user = connection.QueryFirstOrDefault<User>(query, new { mail });
                if (user == null)
                    throw new Exception($"User not found.");

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
                if (!isPasswordValid)
                    throw new Exception("Incorrect Password");
                return user;
            }
        }
    }
}