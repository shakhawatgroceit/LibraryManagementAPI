using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.Entity
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string BookTitle { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Author { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        public int TotalCopies { get; set; }  

        [Required]
        public int AvailableCopies { get; set; } 

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
