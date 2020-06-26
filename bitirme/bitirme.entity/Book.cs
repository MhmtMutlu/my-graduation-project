using System.Collections.Generic;
namespace bitirme.entity
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; } 
        public bool IsHome { get; set; } 
        public List<BookCategory> BookCategories { get; set; }
    }
}