using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using bitirme.entity;

namespace bitirme.webui.Models
{
    public class DepartmentModel
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<Article> Articles { get; set; }
    }
}