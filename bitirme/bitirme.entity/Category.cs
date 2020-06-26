using System.Collections.Generic;

namespace bitirme.entity
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<BookCategory> BookCategories { get; set; }
    }
}