using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories;

public class BlogPostLikeRepository : IBlogPostLikeRepository
{
    private readonly BloggieDbContext _bloggieDbContext;

    public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
    {
        _bloggieDbContext = bloggieDbContext;
    }

    public async Task<int> GetTotalLikesForBlog(Guid blogPostId)
    {
        return await _bloggieDbContext.BlogPostLike
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

        await _bloggieDbContext.BlogPostLike.AddAsync(like);
        await _bloggieDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
    {
        return await _bloggieDbContext.BlogPostLike.Where(x => x.BlogPostId == blogPostId)
            .ToListAsync();
    }
}