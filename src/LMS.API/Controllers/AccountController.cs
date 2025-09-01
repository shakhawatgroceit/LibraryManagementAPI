using LMS.API.Data;
using LMS.API.Models.Dtos;
using LMS.API.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LMS.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerRequest.Email))
                return BadRequest(new { message = "Email already exists" });

            // Hash the password before saving
            var passwordHash = HashPassword(registerRequest.Password);

            var user = new User
            {
                Name = registerRequest.Name,
                Email = registerRequest.Email,
                PasswordHash = passwordHash,
                Role = registerRequest.Role,
                CreateAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });

        }

        // Simple password hashing using SHA256
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // Checks if user exists by email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }                

            // Hash the entered password
            var hashedInputPassword = HashPassword(loginRequest.Password);

            // Compare with stored hash
            if (user.PasswordHash != hashedInputPassword)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }        

            // Success
            return Ok(new { message = "Login Successful" });
        }
    }
}
