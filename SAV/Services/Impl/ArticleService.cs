using SAV.Data;
using SAV.Models;
using SAV.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace SAV.Services.Impl
{
    public class ArticleService : IArticleService
    {
        private readonly AppDbContext _context;

        public ArticleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Article> CreateArticleAsync(ArticleRequest articleRequest)
        {
            // Map ArticleRequest to Article
            var article = new Article
            {
                Name = articleRequest.Name,
                IsUnderWarranty = articleRequest.IsUnderWarranty,
                Price = articleRequest.Price,
                WarrantyEndDate = articleRequest.WarrantyEndDate
            };

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return article; // Return the saved Article with the generated Id
        }

        public async Task<Article> GetArticleByIdAsync(int id)
        {
            return await _context.Articles.FindAsync(id);
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _context.Articles.ToListAsync();
        }

        public async Task<bool> DeleteArticleAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
                return false;

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
