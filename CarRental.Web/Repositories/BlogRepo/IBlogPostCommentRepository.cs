using CarRental.Web.Models.Domain;
using CarRental.Web.Models.Domain.Blog;

namespace CarRental.Web.Repositories.BlogRepo;

public interface IBlogPostCommentRepository
{
    Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);
    Task<IEnumerable<BlogPostComment>> GetAllAsync(Guid blogPostId);
}