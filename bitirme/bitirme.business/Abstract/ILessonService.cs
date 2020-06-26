using System.Collections.Generic;
using bitirme.entity;

namespace bitirme.business.Abstract
{
    public interface ILessonService
    {
        Lesson GetById(int id);
        List<Lesson> GetAll();
        Lesson GetByIdWitNotes(int lessonId);
        void Create(Lesson entity);
        void Update(Lesson entity);
        void Delete(Lesson entity);
    }
}