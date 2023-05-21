using CarRental.Web.Data;
using CarRental.Web.Models.Domain.Blog;
using CarRental.Web.Models.Domain.CarOffer;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Repositories.CarBDRepo;

public class CarTagRepository : ICarTagRepository
{
    private readonly CarDbContext _carDbContext;

    public CarTagRepository(CarDbContext carDbContext)
    {
        _carDbContext = carDbContext;
    }

    public async Task<IEnumerable<CarTag>> GetAllAsync()
    {
        var tags = await _carDbContext.CarTags.ToListAsync();
        return tags.DistinctBy(x => x.Name.ToLower());
    }
}