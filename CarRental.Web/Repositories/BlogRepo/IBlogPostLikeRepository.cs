using CarRental.Web.Models.Domain;
using CarRental.Web.Models.Domain.Blog;

namespace CarRental.Web.Repositories.BlogRepo;

public interface IBlogPostLikeRepository
{
    Task<int> GetTotalLikesForBlog(Guid blogPostId);
    Task AddLikeForBlog(Guid blogPostId, Guid userId);

    Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);
}