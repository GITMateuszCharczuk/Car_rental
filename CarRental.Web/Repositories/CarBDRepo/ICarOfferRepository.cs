using CarRental.Web.Models.Domain.CarOffer;

namespace CarRental.Web.Repositories.CarBDRepo;

public interface ICarOfferRepository
{
    Task<IEnumerable<CarOffer>> GetAllAsync();
    Task<IEnumerable<CarOffer>> GetAllAsync(string tagName);
    Task<CarOffer> GetAsync(Guid id);
    Task<CarOffer> GetAsync(string urlHandle);
    Task<CarOffer> AddAsync(CarOffer carOffer);
    Task<CarOffer> UpdateAsync(CarOffer carOffer);
    Task<bool> DeleteAsync(Guid id);
}