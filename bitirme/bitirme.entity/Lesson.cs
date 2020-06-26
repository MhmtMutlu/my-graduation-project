using System.Collections.Generic;

namespace bitirme.entity
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<DepartmentLesson> DepartmentLessons { get; set; }
        public List<LessonNote> LessonNotes { get; set; }
    }
}