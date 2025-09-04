using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.Dtos
{
    public class UpdateBookDto
    {
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
        [Range(1, int.MaxValue)]
        public int TotalCopies { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int AvailableCopies { get; set; }
    }
}
