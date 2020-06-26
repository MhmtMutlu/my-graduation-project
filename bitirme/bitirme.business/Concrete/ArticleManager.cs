using System.Collections.Generic;
using bitirme.business.Abstract;
using bitirme.data.Abstract;
using bitirme.entity;

namespace bitirme.business.Concrete
{
    public class ArticleManager : IArticleService
    {
        IArticleRepository _articleRepository;
        public ArticleManager(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public bool Create(Article entity)
        {
            if (Validation(entity))
            {
                _articleRepository.Create(entity);
                return true;
            }
            return false;
        }

        public void Delete(Article entity)
        {
            _articleRepository.Delete(entity);
        }

        public List<Article> GetAll()
        {
            return _articleRepository.GetAll();
        }

        public Article GetArticleDetails(int articleId)
        {
            return _articleRepository.GetArticleDetails(articleId);
        }

        public Article GetById(int id)
        {
            return _articleRepository.GetById(id);
        }

        public void Update(Article entity)
        {
            _articleRepository.Update(entity);
        }

        public string ErrorMessage { get; set; }
        public bool Validation(Article entity)
        {
            var isValid = true;

            if (string.IsNullOrEmpty(entity.Title))
            {
                ErrorMessage += "Ürün ismi girmelisiniz.\n";
                isValid = false;
            }

            return isValid;
        }
    }
}