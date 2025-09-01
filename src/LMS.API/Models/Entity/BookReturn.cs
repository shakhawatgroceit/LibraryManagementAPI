using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.API.Models.Entity
{
    public class BookReturn
    {
        [Key]
        public int Id { get; set; }

        //FK to User
        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        //FK to Book
        [Required]
        [ForeignKey("BookId")]
        public int BookId { get; set; }
        public virtual Book Books { get; set; } = null!;

        public DateTime ReturnDate { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
