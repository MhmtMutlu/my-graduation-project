using System.Collections.Generic;
using bitirme.entity;

namespace bitirme.webui.Models
{
    public class ArticleModel
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Url { get; set; }
        public string DocUrl { get; set; }
        public List<DepartmentArticle> DepartmentArticle { get; set; }
    }
}