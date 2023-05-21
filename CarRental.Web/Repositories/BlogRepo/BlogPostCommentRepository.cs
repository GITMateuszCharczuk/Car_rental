using CarRental.Web.Data;
using CarRental.Web.Models.Domain;
using CarRental.Web.Models.Domain.Blog;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Repositories.BlogRepo;

public class BlogPostCommentRepository : IBlogPostCommentRepository
{
    private readonly BlogDbContext _blogDbContext;

    public BlogPostCommentRepository(BlogDbContext blogDbContext)
    {
        _blogDbContext = blogDbContext;
    }

    public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
    {
        await _blogDbContext.BlogPostComment.AddAsync(blogPostComment);
        await _blogDbContext.SaveChangesAsync();
        return blogPostComment;
    }

    public async Task<IEnumerable<BlogPostComment>> GetAllAsync(Guid blogPostId)
    {
        return await _blogDbContext.BlogPostComment.Where(x => x.BlogPostId == blogPostId)
            .ToListAsync();
    }
}