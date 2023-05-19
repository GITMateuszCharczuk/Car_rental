using CarRental.Web.Models.Domain;

namespace CarRental.Web.Repositories;

public interface IBlogPostLikeRepository
{
    Task<int> GetTotalLikesForBlog(Guid blogPostId);
    Task AddLikeForBlog(Guid blogPostId, Guid userId);

    Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);
}