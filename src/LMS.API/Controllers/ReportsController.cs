using LMS.API.Data;
using LMS.API.Models.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueBook()
        {
            var today = DateTime.Today;

            var bookList = await _context.BookIssues
                .Where(bi => bi.ReturnDate == null && bi.DueDate < today)
                .Include(bi => bi.Books)
                .Include(bi => bi.User)
                .ToListAsync();

            var overdueBooks = bookList.Select(bi => new OverdueBookDto
            {
                bookId = bi.BookId,
                BookTitle = bi.Books.BookTitle,
                IssueDate = bi.IssueDate,
                DueDate = bi.DueDate,
                DaysOverdue = (today - bi.DueDate).Days
            }).ToList();

            return Ok(new { message="Overdue Book successfully",overdueBooks});
        }

    }
}
