using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using bitirme.data.Abstract;
using bitirme.entity;

namespace bitirme.data.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category, LibraryContext>, ICategoryRepository
    {
        public void DeleteFromCategory(int bookId, int categoryId)
        {
            using (var context = new LibraryContext())
            {
                var cmd = "delete from productcategory where ProductId=@p0 and CategoryId=@p1";
                context.Database.ExecuteSqlRaw(cmd, bookId, categoryId);
            }
        }

        public Category GetByIdWithBooks(int categoryId)
        {
            using (var context = new LibraryContext())
            {
                return context.Categories
                                .Where(i => i.CategoryId == categoryId)
                                .Include(i => i.BookCategories)
                                .ThenInclude(i => i.Book)
                                .FirstOrDefault();
            }
        }

        public List<Book> GetPopularCategories()
        {
            throw new System.NotImplementedException();
        }
    }
}