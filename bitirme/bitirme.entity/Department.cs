using System.Collections.Generic;

namespace bitirme.entity
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public List<DepartmentLesson> DepartmentLessons { get; set; }
        public List<DepartmentArticle> DepartmentArticle { get; set; }
    }
}