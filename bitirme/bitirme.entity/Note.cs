using System.Collections.Generic;

namespace bitirme.entity
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Author { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DocUrl { get; set; }
        public List<LessonNote> LessonNotes { get; set; }
    }
}