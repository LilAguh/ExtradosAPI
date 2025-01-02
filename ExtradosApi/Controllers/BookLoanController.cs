using ExtradosApi.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace ExtradosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookLoanController : ControllerBase
    {
        private readonly BookLoanService _bookLoanService;
        public BookLoanController(BookLoanService bookLoanService)
        {
            _bookLoanService = bookLoanService;
        }

        [HttpPost("loan")]
        [Authorize(Roles = "user")]
        public IActionResult CreateBookLoan([FromBody] BookLoan request)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("sub")?.Value ?? throw new UnauthorizedAccessException());
                if (request.UserID != userId)
                    return Unauthorized("You can only create loans for your own account.");

                var loan = _bookLoanService.CreateBookLoan(request.BookName, userId);
                return CreatedAtAction(nameof(GetLoansByUserId), new { userId }, loan);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}/loans")]
        [Authorize(Roles = "user")]
        public IActionResult GetLoansByUserId(int userId)
        {
            try
            {
                var loans = _bookLoanService.GetBookLoansByUserId(userId);
                return Ok(loans);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{loanId}/return")]
        [Authorize(Roles = "user")]
        public IActionResult ReturnBook(int loanId)
        {
            try
            {
                var result = _bookLoanService.ReturnBook(loanId);
                return result ? Ok("Book returned successfully.") : NotFound("Loan not found.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
