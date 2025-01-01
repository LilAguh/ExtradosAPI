using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IBookLoanDAO
    {
        BookLoan CreateBookLoan(string bookName, int userId, DateTime loanDate, DateTime dueDate);
        List<BookLoan> GetBookLoansByUserId(int userId);
        bool ReturnBook(int loanId);
    }
}
