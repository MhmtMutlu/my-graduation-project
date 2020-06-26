using System.Collections.Generic;
using bitirme.business.Abstract;
using bitirme.data.Abstract;
using bitirme.data.Concrete.EfCore;
using bitirme.entity;

namespace bitirme.business.Concrete
{
    public class BookManager : IBookService
    {
        private IBookRepository _bookRepository;
        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public bool Create(Book entity)
        {
            if (Validation(entity))
            {
                _bookRepository.Create(entity);
                return true;
            }
            return false;
        }

        public void Delete(Book entity)
        {
            // iş kuralları
            _bookRepository.Delete(entity);
        }

        public List<Book> GetAll()
        {
            return _bookRepository.GetAll();
        }

        public Book GetById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public Book GetByIdWithCategories(int id)
        {
            return _bookRepository.GetByIdWithCategories(id);
        }

        public int GetCountByCategory(string category)
        {
            return _bookRepository.GetCountByCategory(category);
        }

        public List<Book> GetHomePageBooks()
        {
            return _bookRepository.GetHomePageBooks();
        }

        public Book GetBookDetails(string url)
        {
            return _bookRepository.GetBookDetails(url);
        }

        public List<Book> GetBooksByCategory(string name, int page, int pageSize)
        {
            return _bookRepository.GetBooksByCategory(name, page, pageSize);
        }

        public List<Book> GetSearchResult(string searchString)
        {
            return _bookRepository.GetSearchResult(searchString);
        }

        public void Update(Book entity)
        {
            _bookRepository.Update(entity);
        }

        public void Update(Book entity, int[] categoryIds)
        {
            _bookRepository.Update(entity, categoryIds);
        }

        public string ErrorMessage { get; set; }
        public bool Validation(Book entity)
        {
            var isValid = true;

            if (string.IsNullOrEmpty(entity.Name))
            {
                ErrorMessage += "Ürün ismi girmelisiniz.\n";
                isValid = false;
            }

            return isValid;
        }
    }
}