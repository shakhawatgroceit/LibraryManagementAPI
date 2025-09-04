using LMS.API.Data;
using LMS.API.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/bookissue")]
    public class BookIssueController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookIssueController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("bookissue/{bookId:int}")]
        public async Task<IActionResult> BookIssue(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null || book.AvailableCopies <= 0)
                return BadRequest("Book not available");

            var userId = 1; // Hardcoded for now (replace with actual JWT claim later)
            var issue = new BookIssue
            {
                UserId = userId,
                BookId = bookId,
                IssueDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(4) // 2 weeks borrow period
            };
            book.AvailableCopies--;
            _context.BookIssues.Add(issue);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Book Issued successfully",book=book.BookTitle});
        }
        [HttpPost("return/{bookId}")]
        public async Task<IActionResult> ReturnBook(int bookId)
        {
            var userId = 1; // Extract from JWT later
            // 1. Find active issue (not yet returned)
            var issue = await _context.BookIssues
                .FirstOrDefaultAsync(b => b.BookId == bookId && b.UserId == userId && b.ReturnDate == null);
            if (issue == null)
            {
                return BadRequest(new { message = "No active issue records found for the book." });
            }
            // 2. Set return date
            issue.ReturnDate = DateTime.UtcNow;
            // 3. Calculate fine (if returned late)
            if (issue.ReturnDate > issue.DueDate)
            {
                int lateDays = (issue.ReturnDate.Value - issue.DueDate).Days;
                issue.FineAmount = lateDays * 5; // ₹5 per day late fine
            }
            // Update book's available copies
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                book.AvailableCopies++;
            }
            // Optional: Add entry in BookReturn table
            var returnEntry = new BookReturn
            {
                UserId = userId,
                BookId = bookId,
                ReturnDate = issue.ReturnDate.Value,
                Status = "Returned"
            };
            _context.BookReturns.Add(returnEntry);
            // Save all changes to database
            await _context.SaveChangesAsync();
            // Return success response
            return Ok(new
            {
                message = "Book return successfully",
                returnedAt = issue.ReturnDate,
                fine = issue.FineAmount
            });
        }
    }
}
    

    

