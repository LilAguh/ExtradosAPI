using DataAccess.Interfaces;
using DataAccess.Models;
using ExtradosApi.Services.Interfaces;

namespace ExtradosApi.Services
{
    public class BookLoanService : IBookLoanService
    {
        private readonly IBookLoanDAO _bookLoanDAO;

        public BookLoanService(IBookLoanDAO bookLoanDAO)
        {
            _bookLoanDAO = bookLoanDAO;
        }

        public BookLoan CreateBookLoan(string bookName, int userId)
        {
            DateTime loanDate = DateTime.Now;
            DateTime dueDate = loanDate.AddDays(5);

            return _bookLoanDAO.CreateBookLoan(bookName, userId, loanDate, dueDate);
        }

        public List<BookLoan> GetBookLoansByUserId(int userId)
        {
            return _bookLoanDAO.GetBookLoansByUserId(userId);
        }

        public bool ReturnBook(int loanId)
        {
            return _bookLoanDAO.ReturnBook(loanId);
        }
    }
}
