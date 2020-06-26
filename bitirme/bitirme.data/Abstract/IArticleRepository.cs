using System.Collections.Generic;
using bitirme.entity;

namespace bitirme.data.Abstract
{
    public interface IArticleRepository : IRepository<Article>
    {
        Article GetArticleDetails(int articleId);
    }
}