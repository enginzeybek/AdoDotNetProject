using System.ComponentModel.DataAnnotations;

namespace AdoDotNetProject.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string PublicationYear { get; set; }

        public int AvailableCopies { get; set; }
    }
}
