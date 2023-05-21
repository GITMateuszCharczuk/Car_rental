using CarRental.Web.Models.Domain;
using CarRental.Web.Models.Domain.Blog;

namespace CarRental.Web.Repositories.BlogRepo;

public interface IBlogPostRepository
{
    Task<IEnumerable<BlogPost>> GetAllAsync();
    Task<IEnumerable<BlogPost>> GetAllAsync(string tagName);
    Task<BlogPost> GetAsync(Guid id);
    Task<BlogPost> GetAsync(string urlHandle);
    Task<BlogPost> AddAsync(BlogPost blogPost);
    Task<BlogPost> UpdateAsync(BlogPost blogPost);
    Task<bool> DeleteAsync(Guid id);
}