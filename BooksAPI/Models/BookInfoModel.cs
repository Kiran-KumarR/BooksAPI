﻿namespace BooksAPI.Models
{
    public class BookInfoModel 
    {
        //this will have the all the clumns when tried to do inner join

        public int id { get; set; }

        public string title { get; set; }

        public int  auth_id { get; set; }

        public string author_name{ get; set; }

        public int pub_id { get; set; }

        public string publisher_name { get; set; }
        public string description { get; set; }
        public string language { get; set; }

        public string maturityRating { get; set; }

        public int  pageCount { get; set; }

        public string categories { get; set; }

        public string publishedDate { get; set; }

        public decimal retailPrice { get; set; }
    }
}
