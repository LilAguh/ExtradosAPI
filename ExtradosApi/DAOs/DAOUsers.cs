using MySqlConnector;
using Dapper;
using ExtradosApi.Models;

namespace ExtradosApi.DAOs
{
    public class DAOUsers
    {
        
        string connectionString = "Server=127.0.0.1;Database=ApiEjercicio;UId=aguh;Password=asd123;";
        public List<Users> ObtenerTodosLosUsuariosRegistrados()
        {
            string query = "SELECT * FROM Users;";
            using (var connection = new MySqlConnection(connectionString))
            {
                var users = connection.Query<Users>(query).AsList();
                return users;
            }
        }
        public List<Users> ObtenerTodosLosUsuarios()
        {
            string query = "SELECT * FROM Users WHERE active = 1;";
            using (var connection = new MySqlConnection(connectionString))
            {
                var users = connection.Query<Users>(query).AsList();
                return users;
            }
        }
        public Users ObtenerUsuario(int id)
        {
            string query = "SELECT * FROM Users where id = @id and active = 1;";
            using (var connection = new MySqlConnection(connectionString))
            {
                var user = connection.QueryFirstOrDefault<Users>(query, new { id = id });
                return user;
            }
        }
        public Users CrearUsuario(string nombre, int edad)
        {
            string query = "INSERT INTO Users (name, age) VALUES (@name, @age);";
            string queryUsuarioCreado = "SELECT * FROM Users WHERE id = LAST_INSERT_ID();";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(query, new { nombre = nombre, edad = edad });
                var user = connection.QueryFirstOrDefault<Users>(queryUsuarioCreado);
                return user;
            }
        }

        public Users ActualizarUsuario(int id, string? name = null, int? age = null)
        {
            string query = "UPDATE Usuarios SET nombre = @nombre, edad = @edad WHERE id = @id;";
            string queryUsuarioActualizado = "SELECT * FROM Usuarios WHERE id = @id;";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                //Se buscan los datos originales y en caso de no quere hacer cambio a alguno se deja el original
                var user = connection.QueryFirstOrDefault<Users>(queryUsuarioActualizado, new { id = id });
                string newName = name ?? user.Name;
                int newAge = age ?? user.Age;


                connection.Execute(query, new { nombre = newName, edad = newAge, id = id });
                var usuarioActualizado = connection.QueryFirstOrDefault<Users>(queryUsuarioActualizado, new { id = id });
                return usuarioActualizado;
            }
        }
        public bool DesactivarUsuario(int id)
        {
            string query = "UPDATE Usuarios SET activo = 0 WHERE id = @id;";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                int usuarioEliminado = connection.Execute(query, new { id });
                return usuarioEliminado > 0;
            }
        }

    }
}

