using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.API.Models.Entity
{
    public class BookIssue
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        [Required]
        [ForeignKey("BookId")]
        public int BookId { get; set; }
        public virtual Book Books { get; set; } = null!;

        [Required]
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }  

        [Column(TypeName = "decimal(10,2)")]
        public decimal FineAmount { get; set; } = 0;  
    }
}
