namespace bitirme.entity
{
    public class DepartmentArticle
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}