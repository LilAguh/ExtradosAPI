using DataAccess.Models;

namespace ExtradosApi.Services.Interfaces
{
    public interface IBookLoanService
    {
        BookLoan CreateBookLoan(string bookName, int userId, DateTime loanDate, DateTime dueDate);
        List<BookLoan> GetBookLoansByUserId(int userId);
        bool ReturnBook(int loanId);
    }
}
