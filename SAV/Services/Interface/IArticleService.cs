using SAV.Models;

namespace SAV.Services.Interface
{
        public interface IArticleService
        {
            Task<Article> CreateArticleAsync(ArticleRequest article);
            Task<Article> GetArticleByIdAsync(int id);
            Task<IEnumerable<Article>> GetAllArticlesAsync();
            Task<bool> DeleteArticleAsync(int id);
        }
}
