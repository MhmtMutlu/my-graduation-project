using System.Collections.Generic;
using bitirme.entity;

namespace bitirme.data.Abstract
{
    public interface IDepartmentRepository:IRepository<Department>
    {
        Department GetByIdWithLesson(int departmentId);
        Department GetByIdWithArticles(int departmentId);
    }
}