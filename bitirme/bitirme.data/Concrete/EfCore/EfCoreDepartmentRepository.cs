using System.Collections.Generic;
using System.Linq;
using bitirme.data.Abstract;
using bitirme.entity;
using Microsoft.EntityFrameworkCore;

namespace bitirme.data.Concrete.EfCore
{
    public class EfCoreDepartmentRepository : EfCoreGenericRepository<Department, DepartmentContext>, IDepartmentRepository
    {
        public Department GetByIdWithArticles(int departmentId)
        {
            using (var context = new DepartmentContext())
            {
                return context.Departments
                                .Where(i => i.DepartmentId == departmentId)
                                .Include(i => i.DepartmentArticle)
                                .ThenInclude(i => i.Article)
                                .FirstOrDefault();
            }
        }

        public Department GetByIdWithLesson(int departmentId)
        {
            using (var context = new DepartmentContext())
            {
                return context.Departments
                                .Where(i => i.DepartmentId == departmentId)
                                .Include(i => i.DepartmentLessons)
                                .ThenInclude(i => i.Lesson)
                                .FirstOrDefault();
            }
        }
    }
}