namespace bitirme.entity
{
    public class LessonNote
    {
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}