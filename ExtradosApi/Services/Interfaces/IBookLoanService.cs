using DataAccess.Models;

namespace ExtradosApi.Services.Interfaces
{
    public interface IBookLoanService
    {
        BookLoan CreateBookLoan(string bookName, int userId);
        List<BookLoan> GetBookLoansByUserId(int userId);
        bool ReturnBook(int loanId);
    }
}