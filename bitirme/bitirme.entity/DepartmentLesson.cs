namespace bitirme.entity
{
    public class DepartmentLesson
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}