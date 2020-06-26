using System.Collections.Generic;
using bitirme.entity;

namespace bitirme.data.Abstract
{
    public interface ICategoryRepository:IRepository<Category>
    {
        Category GetByIdWithBooks(int categoryId);
        void DeleteFromCategory(int bookId, int categoryId);
    }
}