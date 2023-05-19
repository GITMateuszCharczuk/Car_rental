using CarRental.Web.Data;
using CarRental.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Repositories;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly BlogDbContext _blogDbContext;

    public BlogPostRepository(BlogDbContext blogDbContext)
    {
        _blogDbContext = blogDbContext;
    }

    public async Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        return await _blogDbContext.BlogPosts.Include(nameof(BlogPost.Tags)).ToListAsync();
    }

    public async Task<IEnumerable<BlogPost>> GetAllAsync(string tagName)
    {
        return await _blogDbContext.BlogPosts.Include(nameof(BlogPost.Tags))
            .Where(x => x.Tags.Any(x => x.Name == tagName))
            .ToListAsync();
    }

    public async Task<BlogPost> GetAsync(Guid id)
    {
        return await _blogDbContext.BlogPosts.Include(nameof(BlogPost.Tags)).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BlogPost> GetAsync(string urlHandle)
    {
        return await _blogDbContext.BlogPosts.Include(nameof(BlogPost.Tags))
            .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
    }

    public async Task<BlogPost> AddAsync(BlogPost blogPost)
    {
        await _blogDbContext.BlogPosts.AddAsync(blogPost);
        await _blogDbContext.SaveChangesAsync();
        return blogPost;
    }

    public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
    {
        var existingBlogPost = await _blogDbContext.BlogPosts.Include(nameof(BlogPost.Tags))
            .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

        if (existingBlogPost != null)
        {
            existingBlogPost.Heading = blogPost.Heading;
            existingBlogPost.PageTitle = blogPost.PageTitle;
            existingBlogPost.Author = blogPost.Author;
            existingBlogPost.Content = blogPost.Content;
            existingBlogPost.ShortDescription = blogPost.ShortDescription;
            existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
            existingBlogPost.UrlHandle = blogPost.UrlHandle;
            existingBlogPost.PublishedDate = blogPost.PublishedDate;
            existingBlogPost.Visible = blogPost.Visible;


            if (blogPost.Tags != null && blogPost.Tags.Any())
            {
                // Delete the existing tags
                _blogDbContext.Tags.RemoveRange(existingBlogPost.Tags);

                // Add new tags
                blogPost.Tags.ToList().ForEach(x => x.BlogPostId = existingBlogPost.Id);
                await _blogDbContext.Tags.AddRangeAsync(blogPost.Tags);
            }
        }

        await _blogDbContext.SaveChangesAsync();
        return existingBlogPost;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existingBlogPost = await _blogDbContext.BlogPosts.FindAsync(id);
        if (existingBlogPost != null)
        {
            _blogDbContext.BlogPosts.Remove(existingBlogPost);
            await _blogDbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
}