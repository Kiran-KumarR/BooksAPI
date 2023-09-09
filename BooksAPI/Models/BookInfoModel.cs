namespace BooksAPI.Models
{
    public class BookInfoModel
    {

        public int id { get; set; }

        public string language { get; set; }

        public string maturityRating { get; set; }

        public string pageCount { get; set; }

        public string categories { get; set; }

        public string publishedDate { get; set; }

        public decimal retailPrice { get; set; }
    }
}
