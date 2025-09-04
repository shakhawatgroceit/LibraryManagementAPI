namespace LMS.API.Models.Dtos
{
    public class GetAllBookDto
    {
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }

        public string ISBN { get; set; }

        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }
    }
}
