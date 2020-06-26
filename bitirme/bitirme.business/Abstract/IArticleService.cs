using System.Collections.Generic;
using bitirme.entity;

namespace bitirme.business.Abstract
{
    public interface IArticleService:IValidator<Article>
    {
        Article GetById(int id);
        List<Article> GetAll();
        Article GetArticleDetails(int articleId);
        bool Create(Article entity);
        void Update(Article entity);
        void Delete(Article entity);
    }
}