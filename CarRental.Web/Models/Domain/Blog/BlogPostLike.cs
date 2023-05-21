namespace CarRental.Web.Models.Domain.Blog;

public class BlogPostLike
{
    public Guid Id { get; set; }
    public Guid BlogPostId { get; set; }
    public Guid UserId { get; set; }
}