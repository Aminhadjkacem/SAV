using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAV.Data;
using SAV.Models;
using SAV.Services.Interface;


namespace SAV.Controllers
{/*
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ArticleController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Article
        [HttpPost]
        [Authorize(Roles = "SAV")] // Only accessible by users with "SAV" role
        public async Task<IActionResult> CreateArticle([FromBody] Article article)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetArticle", new { id = article.Id }, article);
        }

        // GET: api/Article/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        // GET: api/Article
        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _context.Articles.ToListAsync();
            return Ok(articles); // Accessible by everyone
        }

        // DELETE: api/Article/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "SAV")] // Only accessible by users with "SAV" role
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);

            if (article == null)
            {
                return NotFound(); // Return 404 if the article does not exist
            }

            _context.Articles.Remove(article); // Remove the article from the DbSet
            await _context.SaveChangesAsync(); // Save changes to the database

            return NoContent(); // Return 204 No Content status code
        }
    }*/
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        // POST: api/Article
        [HttpPost]
        [Authorize(Roles = "SAV")] // Only accessible by users with "SAV" role
        public async Task<IActionResult> CreateArticle([FromBody] ArticleRequest article)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdArticle = await _articleService.CreateArticleAsync(article);
            return CreatedAtAction(nameof(GetArticle), new { id = createdArticle.Id }, createdArticle);
        }

        // GET: api/Article/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);
            if (article == null)
                return NotFound();

            return Ok(article);
        }

        // GET: api/Article
        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _articleService.GetAllArticlesAsync();
            return Ok(articles); // Accessible by everyone
        }

        // DELETE: api/Article/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "SAV")] // Only accessible by users with "SAV" role
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var isDeleted = await _articleService.DeleteArticleAsync(id);
            if (!isDeleted)
                return NotFound();

            return NoContent(); // Return 204 No Content status code
        }
    }
}
