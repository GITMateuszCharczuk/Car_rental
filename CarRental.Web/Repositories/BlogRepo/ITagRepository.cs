using CarRental.Web.Models.Domain;
using CarRental.Web.Models.Domain.Blog;

namespace CarRental.Web.Repositories.BlogRepo;

public interface ITagRepository
{
    Task<IEnumerable<Tag>> GetAllAsync();
}