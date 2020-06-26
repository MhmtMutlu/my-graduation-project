using System.Collections.Generic;
using bitirme.entity;

namespace bitirme.business.Abstract
{
    public interface IDepartmentService:IValidator<Department>
    {
        Department GetById(int id);
        List<Department> GetAll();
        Department GetByIdWithLesson(int departmentId);
        Department GetByIdWithArticles(int departmentId);
        void Create(Department entity);
        void Update(Department entity);
        void Delete(Department entity);
    }
}