using System.Collections.Generic;
using bitirme.entity;

namespace bitirme.data.Abstract
{
    public interface ILessonRepository:IRepository<Lesson>
    {
        Lesson GetByIdWitNotes(int lessonId);
    }
}