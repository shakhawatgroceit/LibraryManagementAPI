using LMS.API.Data;
using LMS.API.Models.Dtos;
using LMS.API.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllBooks()
        {
            //var books = await _context.Books.Select(b => new GetAllBookDto
            //{
            //    BookTitle = b.BookTitle,
            //    Author = b.Author,
            //    Category = b.Category,
            //    ISBN = b.ISBN,
            //    TotalCopies = b.TotalCopies,
            //    AvailableCopies = b.AvailableCopies,
            //}).ToListAsync();
            //return Ok(books);
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }

        [HttpPost("create")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateBook([FromBody] BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var book = new Book
            {
                BookTitle = bookDto.BookTitle,
                Author = bookDto.Author,
                Category = bookDto.Category,
                ISBN = bookDto.ISBN,
                TotalCopies = bookDto.TotalCopies,
                AvailableCopies = bookDto.AvailableCopies,
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Book Add successfully" });
        }

        [HttpPut("update/{id:int}")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Book not found" });
            }
            book.BookTitle = updateBookDto.BookTitle;
            book.Author = updateBookDto.Author;
            book.Category = updateBookDto.Category;
            book.ISBN = updateBookDto.ISBN;
            book.TotalCopies = updateBookDto.TotalCopies;
            book.AvailableCopies = updateBookDto.AvailableCopies;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Book updated successfully", book });
        }

        [HttpDelete("delete/{id:int}")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Book not found" });
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Book Deleted Successfully" });
        }

        [HttpGet("get/{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Book not found" });
            }
            return Ok(book);
        }
    }
}
