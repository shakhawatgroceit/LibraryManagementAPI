namespace LMS.API.Models.Dtos
{
    public class OverdueBookDto
    {
        public int bookId { get; set; }
        public string BookTitle { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public int DaysOverdue { get; set; }
    }
}
