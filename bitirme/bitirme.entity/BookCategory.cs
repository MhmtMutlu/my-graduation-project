namespace bitirme.entity
{
    public class BookCategory
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}