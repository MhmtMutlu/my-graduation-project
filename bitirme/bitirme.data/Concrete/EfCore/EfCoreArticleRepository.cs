using System.Collections.Generic;
using System.Linq;
using bitirme.data.Abstract;
using bitirme.entity;

namespace bitirme.data.Concrete.EfCore
{
    public class EfCoreArticleRepository : EfCoreGenericRepository<Article, DepartmentContext>, IArticleRepository
    {
        public Article GetArticleDetails(int articleId)
        {
            using (var context = new DepartmentContext())
            {
                return context.Articles
                                .Where(i => i.ArticleId == articleId)
                                .FirstOrDefault();
            }
        }
    }
}