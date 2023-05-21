using CarRental.Web.Data;
using CarRental.Web.Models.Domain;
using CarRental.Web.Models.Domain.Blog;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Repositories.BlogRepo;

public class TagRepository : ITagRepository
{
    private readonly BlogDbContext _blogDbContext;

    public TagRepository(BlogDbContext blogDbContext)
    {
        _blogDbContext = blogDbContext;
    }

    public async Task<IEnumerable<Tag>> GetAllAsync()
    {
        var tags = await _blogDbContext.Tags.ToListAsync();
        return tags.DistinctBy(x => x.Name.ToLower());
    }
}