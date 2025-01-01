using DataAccess.Interfaces;
using DataAccess.Models;
using MySqlConnector;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Implementations
{
    public class BookLoanDAO : IBookLoanDAO
    {
        private readonly string connectionString;

        public BookLoanDAO (IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public BookLoan CreateBookLoan (string bookName, int userId, DateTime loanDate, DateTime dueDate)
        {
            string query = "INSERT INTO BookLoans (bookname, userid, loandate, duedate) VALUES (@BookName, @UserID, @LoanDate, @DueDate);";
            string queryLastLoan = "SELECT * FROM BookLoans WHERE ID = LAST_INSERT_ID();";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(query, new { BookName = bookName, UserID = userId, LoanDate = loanDate, DueDate = dueDate });
                return connection.QueryFirstOrDefault<BookLoan>(queryLastLoan);
            }
        }

        public List<BookLoan> GetBookLoansByUserId( int userId)
        {
            string query = "SELECT * FROM BookLoans WHERE userid = @UserId";
            using (var connection = new MySqlConnection(connectionString))
            {
                return connection.Query<BookLoan>(query, new { UserID = userId }).AsList();
            }
        }

        public bool ReturnBook( int loanId)
        {
            string query = "UPDATE BookLoans SET returned = 1 WHERE id = @LoanID;";
            using (var connection = new MySqlConnection(connectionString))
            {
                return connection.Execute(query, new { LoanID = loanId }) > 0;
            }
        }
    }
}
