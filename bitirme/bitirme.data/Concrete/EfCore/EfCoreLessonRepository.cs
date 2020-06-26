using System.Collections.Generic;
using System.Linq;
using bitirme.data.Abstract;
using bitirme.entity;
using Microsoft.EntityFrameworkCore;

namespace bitirme.data.Concrete.EfCore
{
    public class EfCoreLessonRepository : EfCoreGenericRepository<Lesson, DepartmentContext>, ILessonRepository
    {
        public Lesson GetByIdWitNotes(int lessonId)
        {
            using (var context = new DepartmentContext())
            {
                return context.Lessons
                                .Where(i => i.LessonId == lessonId)
                                .Include(i => i.LessonNotes)
                                .ThenInclude(i => i.Note)
                                .FirstOrDefault();
            }
        }
    }
}