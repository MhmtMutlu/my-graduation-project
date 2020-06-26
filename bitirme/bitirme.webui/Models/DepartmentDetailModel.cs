using System.Collections.Generic;
using bitirme.entity;

namespace bitirme.webui.Models
{
    public class DepartmentDetailModel
    {
        public Department Department { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<Article> Articles { get; set; }
    }
}