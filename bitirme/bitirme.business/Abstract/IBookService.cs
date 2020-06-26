using System.Collections.Generic;
using bitirme.entity;

namespace bitirme.business.Abstract
{
    public interface IBookService: IValidator<Book> // IValidator olmayabilir.
    {
        Book GetById(int id);
        Book GetByIdWithCategories(int id);
        Book GetBookDetails(string url);
        List<Book> GetBooksByCategory(string name, int page, int pageSize);
        List<Book> GetAll();
        List<Book> GetHomePageBooks();
        List<Book> GetSearchResult(string searchString);
        bool Create(Book entity);
        void Update(Book entity);
        void Delete(Book entity);
        int GetCountByCategory(string category);
        void Update(Book entity, int[] categoryIds);
    }
}