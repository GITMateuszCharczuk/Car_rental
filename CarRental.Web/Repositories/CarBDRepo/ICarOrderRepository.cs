using CarRental.Web.Models.Domain.CarOffer;

namespace CarRental.Web.Repositories.CarBDRepo;

public interface ICarOrderRepository
{
    Task<IEnumerable<CarOrder>> GetAllAsync();
    Task<CarOrder> GetAsync(Guid id);
    Task<CarOrder> AddAsync(CarOrder carOrder);
    Task<bool> DeleteAsync(Guid id);
}