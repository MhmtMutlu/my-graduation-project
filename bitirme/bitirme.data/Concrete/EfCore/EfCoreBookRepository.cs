using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using bitirme.data.Abstract;
using bitirme.entity;

namespace bitirme.data.Concrete.EfCore
{
    public class EfCoreBookRepository : EfCoreGenericRepository<Book, LibraryContext>, IBookRepository
    {
        public Book GetByIdWithCategories(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.Books
                                .Where(i => i.BookId == id)
                                .Include(i => i.BookCategories)
                                .ThenInclude(i => i.Category)
                                .FirstOrDefault();
            }
        }

        public int GetCountByCategory(string category)
        {
            using (var context = new LibraryContext())
            {
                var books = context
                                .Books
                                .Where(i => i.IsApproved)
                                .AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    books = books
                                .Include(i => i.BookCategories)
                                .ThenInclude(i => i.Category)
                                .Where(i => i.BookCategories.Any(a => a.Category.Url == category));
                }
                return books.Count();
            }
        }

        public List<Book> GetHomePageBooks()
        {
            using (var context = new LibraryContext())
            {
                return context.Books
                                .Where(i => i.IsApproved && i.IsHome)
                                .ToList();
            }
        }

        public List<Book> GetPopularBooks()
        {
            using (var context = new LibraryContext())
            {
                return context.Books.ToList();
            }
        }

        public Book GetBookDetails(string url)
        {
            using (var context = new LibraryContext())
            {
                return context.Books
                                .Where(i => i.Url == url)
                                .Include(i => i.BookCategories)
                                .ThenInclude(i => i.Category)
                                .FirstOrDefault();
            }
        }

        public List<Book> GetBooksByCategory(string name, int page, int pageSize)
        {
            using (var context = new LibraryContext())
            {
                var books = context
                                .Books
                                .Where(i => i.IsApproved)
                                .AsQueryable();
                                
                if (!string.IsNullOrEmpty(name))
                {
                    books = books
                                .Include(i => i.BookCategories)
                                .ThenInclude(i => i.Category)
                                .Where(i => i.BookCategories.Any(a => a.Category.Url == name));
                }
                return books.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public List<Book> GetSearchResult(string searchString)
        {
            using (var context = new LibraryContext())
            {
                var books = context
                                .Books
                                .Where(i => i.IsApproved && (i.Name.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower()))) 
                                .AsQueryable();
                
                return books.ToList();
            }
        }

        public void Update(Book entity, int[] categoryIds)
        {
            using (var context = new LibraryContext())
            {
                var book = context.Books
                                    .Include(i => i.BookCategories)
                                    .FirstOrDefault(i => i.BookId == entity.BookId);

                if (book != null)
                {
                    book.Name = entity.Name;
                    book.Stock = entity.Stock;
                    book.Description = entity.Description;
                    book.Url = entity.Url;
                    book.ImageUrl = entity.ImageUrl;
                    book.IsApproved = entity.IsApproved;
                    book.IsHome = entity.IsHome;
                    book.BookCategories = categoryIds.Select(catid => new BookCategory()
                    {
                        BookId = entity.BookId,
                        CategoryId = catid
                    }).ToList();

                    context.SaveChanges();
                }
            }
        }
    }
}