using System.Collections.Generic;
using bitirme.entity;

namespace bitirme.webui.Models
{
    public class LessonModel
    {
        public int LessonId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Note> Notes { get; set; }
    }
}