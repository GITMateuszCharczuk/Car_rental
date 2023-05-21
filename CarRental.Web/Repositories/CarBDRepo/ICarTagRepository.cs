using CarRental.Web.Models.Domain.CarOffer;

namespace CarRental.Web.Repositories.CarBDRepo;

public interface ICarTagRepository
{
    Task<IEnumerable<CarTag>> GetAllAsync();
}