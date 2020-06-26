using System.Collections.Generic;

namespace bitirme.entity
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Url { get; set; }
        public string DocUrl { get; set; }
        public List<DepartmentArticle> DepartmentArticle { get; set; }
    }
}