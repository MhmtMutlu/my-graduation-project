using System.Collections.Generic;
using bitirme.business.Abstract;
using bitirme.data.Abstract;
using bitirme.entity;

namespace bitirme.business.Concrete
{
    public class DepartmentManager : IDepartmentService
    {
        private IDepartmentRepository _departmentRepository;
        public DepartmentManager(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public string ErrorMessage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void Create(Department entity)
        {
            _departmentRepository.Create(entity);
        }

        public void Delete(Department entity)
        {
            _departmentRepository.Delete(entity);
        }

        public List<Department> GetAll()
        {
            return _departmentRepository.GetAll();
        }

        public Department GetByIdWithLesson(int departmentId)
        {
            return _departmentRepository.GetByIdWithLesson(departmentId);
        }

        public Department GetById(int id)
        {
            return _departmentRepository.GetById(id);
        }

        public void Update(Department entity)
        {
            _departmentRepository.Update(entity);
        }

        public bool Validation(Department entity)
        {
            throw new System.NotImplementedException();
        }

        public Department GetByIdWithArticles(int departmentId)
        {
            return _departmentRepository.GetByIdWithArticles(departmentId);
        }
    }
}