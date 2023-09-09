namespace BooksAPI.Models
{
    public class BooksModel
    {

        public int id { get; set; }

        public string title { get; set; }

        public int author_id { get; set; }
        public int publisher_id { get; set; }

        public string description { get; set; }
    }
}
