using CarRental.Web.Data;
using CarRental.Web.Models.Domain;
using CarRental.Web.Models.Domain.Blog;
using CarRental.Web.Models.Domain.CarOffer;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Repositories.CarBDRepo;

public class CarOfferRepository : ICarOfferRepository
{
    private readonly CarDbContext _carDbContext;

    public CarOfferRepository(CarDbContext carDbContext)
    {
        _carDbContext = carDbContext;
    }

    public async Task<IEnumerable<CarOffer>> GetAllAsync()
    {
        return await _carDbContext.CarOffers
            .Include(nameof(CarOffer.Tags))
            .Include(nameof(CarOffer.ImageUrls))
            .Include(nameof(CarOffer.Tarrifs))
            .ToListAsync();
    }

    public async Task<IEnumerable<CarOffer>> GetAllAsync(string tagName)
    {
        return await _carDbContext.CarOffers
            .Include(nameof(CarOffer.ImageUrls))
            .Include(nameof(CarOffer.Tarrifs))
            .Include(nameof(BlogPost.Tags))
            .Where(x => x.Tags.Any(x => x.Name == tagName))
            .ToListAsync();
    }

    public async Task<CarOffer> GetAsync(Guid id)
    {
        return await _carDbContext.CarOffers
            .Include(nameof(CarOffer.Tags))
            .Include(nameof(CarOffer.ImageUrls))
            .Include(nameof(CarOffer.Tarrifs))
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<CarOffer> GetAsync(string urlHandle)
    {
        return await _carDbContext.CarOffers
            .Include(nameof(CarOffer.Tags))
            .Include(nameof(CarOffer.ImageUrls))
            .Include(nameof(CarOffer.Tarrifs))
            .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
    }

    public async Task<CarOffer> AddAsync(CarOffer carOffer)
    {
        await _carDbContext.CarOffers.AddAsync(carOffer);
        await _carDbContext.SaveChangesAsync();
        return carOffer;
    }

    public async Task<CarOffer?> UpdateAsync(CarOffer carOffer)
    {
        var existingCarOffer = await _carDbContext.CarOffers
            .Include(nameof(CarOffer.Tags))
            .Include(nameof(CarOffer.ImageUrls))
            .Include(nameof(CarOffer.Tarrifs))
            .FirstOrDefaultAsync(x => x.Id == carOffer.Id);
        if (existingCarOffer != null)
        {
            existingCarOffer.Id = carOffer.Id;
            existingCarOffer.Heading = carOffer.Heading;
            existingCarOffer.ShortDescription = carOffer.ShortDescription;
            existingCarOffer.FeaturedImageUrl = carOffer.FeaturedImageUrl;
            existingCarOffer.UrlHandle = carOffer.UrlHandle;
            existingCarOffer.Horsepower = carOffer.Horsepower;
            existingCarOffer.YearOfProduction = carOffer.YearOfProduction;
            existingCarOffer.EngineDetails = carOffer.EngineDetails;
            existingCarOffer.DriveDetails = carOffer.DriveDetails;
            existingCarOffer.GearboxDetails = carOffer.GearboxDetails;
            existingCarOffer.CarDeliverylocation = carOffer.CarDeliverylocation;
            existingCarOffer.CarReturnLocation = carOffer.CarReturnLocation;
            existingCarOffer.PublishedDate = carOffer.PublishedDate;
            existingCarOffer.Visible = carOffer.Visible;
            existingCarOffer.Tarrifs = carOffer.Tarrifs;
            
            if (carOffer.Tags != null && carOffer.Tags.Any())
            {
                // Delete the existing tags
                _carDbContext.CarTags.RemoveRange(existingCarOffer.Tags);
                
                // Add new tags
                carOffer.Tags.ToList().ForEach(x => x.CarOfferId = existingCarOffer.Id);
                await _carDbContext.CarTags.AddRangeAsync(carOffer.Tags);
            }
            
            if (carOffer.ImageUrls != null && carOffer.ImageUrls.Any())
            {
                // Delete the existing tags
                _carDbContext.ImageUrls.RemoveRange(existingCarOffer.ImageUrls);
                
                // Add new tags
                carOffer.ImageUrls.ToList().ForEach(x => x.CarOfferId = existingCarOffer.Id);
                await _carDbContext.ImageUrls.AddRangeAsync(carOffer.ImageUrls);
            }
        }
        else
        {
        }
        
        await _carDbContext.SaveChangesAsync();
        return existingCarOffer;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existingCarOffer = await _carDbContext.CarOffers.FindAsync(id);
        if (existingCarOffer != null)
        {
            _carDbContext.CarOffers.Remove(existingCarOffer);
            await _carDbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
}