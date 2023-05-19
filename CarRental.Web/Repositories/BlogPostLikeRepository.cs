using CarRental.Web.Data;
using CarRental.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Repositories;

public class BlogPostLikeRepository : IBlogPostLikeRepository
{
    private readonly BlogDbContext _blogDbContext;

    public BlogPostLikeRepository(BlogDbContext blogDbContext)
    {
        _blogDbContext = blogDbContext;
    }

    public async Task<int> GetTotalLikesForBlog(Guid blogPostId)
    {
        return await _blogDbContext.BlogPostLike
            .CountAsync(x => x.BlogPostId == blogPostId);
    }

    public async Task AddLikeForBlog(Guid blogPostId, Guid userId)
    {
        var like = new BlogPostLike
        {
            Id = Guid.NewGuid(),
            BlogPostId = blogPostId,
            UserId = userId
        };

        await _blogDbContext.BlogPostLike.AddAsync(like);
        await _blogDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
    {
        return await _blogDbContext.BlogPostLike.Where(x => x.BlogPostId == blogPostId)
            .ToListAsync();
    }
}