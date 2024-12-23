using DataAccess.Interfaces;
using DataAccess.Models;
using MySqlConnector;
using Dapper;

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

        public User CreateUser(string name, string mail, int age)
        {
            string query = "INSERT INTO Users (name, mail, age) VALUES (@name, @mail, @age);";
            string queryUserCreated = "SELECT * FROM Users WHERE id = LAST_INSERT_ID();";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(query, new { name, mail, age });
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

        public User GetUser(int id)
        {
            string query = "SELECT * FROM Users WHERE id = @id;";
            using (var connection = new MySqlConnection(connectionString))
            {
                // Ejecutar la consulta y devolver el primer resultado
                return connection.QueryFirstOrDefault<User>(query, new { id });
            }
        }
    }
}