using CarRental.Web.Models.Domain;

namespace CarRental.Web.Repositories;

public interface ITagRepository
{
    Task<IEnumerable<Tag>> GetAllAsync();
}