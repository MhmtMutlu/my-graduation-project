using System.Collections.Generic;
using bitirme.entity;

namespace bitirme.data.Abstract
{
    public interface IBookRepository:IRepository<Book>
    {
        Book GetBookDetails(string url);
        Book GetByIdWithCategories(int id);
        List<Book> GetBooksByCategory(string name, int page, int pageSize);
        List<Book> GetSearchResult(string searchString);
        List<Book> GetHomePageBooks();
        int GetCountByCategory(string category);
        void Update(Book entity, int[] categoryIds);
    }
}