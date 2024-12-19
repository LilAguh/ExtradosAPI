using MySqlConnector;
using Dapper;
using ExtradosApi.Models;

namespace ExtradosApi.DAOs
{
    public class DAOUsers
    {
        private static DAOUsers _instance; // Única instancia de la clase
        private static readonly object _lock = new object(); // Objeto para sincronización

        private string connectionString = "Server=127.0.0.1;Database=ApiEjercicio;UId=aguh;Password=asd123;";

        // Constructor privado para evitar instanciación directa
        private DAOUsers() { }

        // Propiedad para obtener la instancia única
        public static DAOUsers Instance
        {
            get
            {
                lock (_lock) // Bloqueo para garantizar que solo un hilo cree la instancia
                {
                    if (_instance == null)
                    {
                        _instance = new DAOUsers();
                    }
                }
                return _instance;
            }
        }

        // Métodos CRUD
        public List<Users> GetAllUsersRegistered()
        {
            string query = "SELECT * FROM Users;";
            using (var connection = new MySqlConnection(connectionString))
            {
                var users = connection.Query<Users>(query).AsList();
                return users;
            }
        }

        public List<Users> GetAllUsersActive()
        {
            string query = "SELECT * FROM Users WHERE active = 1;";
            using (var connection = new MySqlConnection(connectionString))
            {
                var users = connection.Query<Users>(query).AsList();
                return users;
            }
        }

        public Users GetUser(int id)
        {
            string query = "SELECT * FROM Users where id = @id and active = 1;";
            using (var connection = new MySqlConnection(connectionString))
            {
                var user = connection.QueryFirstOrDefault<Users>(query, new { id = id });
                return user;
            }
        }

        public Users CreateUser(string name, string mail, int age)
        {
            string query = "INSERT INTO Users (name, mail, age) VALUES (@name, @mail, @age);";
            string queryUserCreated = "SELECT * FROM Users WHERE id = LAST_INSERT_ID();";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(query, new { name = name, mail = mail, age = age });
                var user = connection.QueryFirstOrDefault<Users>(queryUserCreated);
                return user;
            }
        }

        public Users UpdateUser(int id, string? name = null, int? age = null, string? mail = null)
        {
            string query = "UPDATE User SET name = @name, age = @age, mail = @mail WHERE id = @id;";
            string queryUpdateUser = "SELECT * FROM User WHERE id = @id;";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var user = connection.QueryFirstOrDefault<Users>(queryUpdateUser, new { id = id });
                string newName = name ?? user.Name;
                int newAge = age ?? user.Age;
                string newMail = mail ?? user.Mail;

                connection.Execute(query, new { name = newName, age = newAge, mail = newMail, id = id });
                var userUpdate = connection.QueryFirstOrDefault<Users>(queryUpdateUser, new { id = id });
                return userUpdate;
            }
        }

        public bool DesactivateUser(int id)
        {
            string query = "UPDATE User SET active = 0 WHERE id = @id;";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                int userDeleted = connection.Execute(query, new { id });
                return userDeleted > 0;
            }
        }
    }
}

