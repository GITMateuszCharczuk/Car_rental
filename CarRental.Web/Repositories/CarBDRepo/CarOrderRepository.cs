using CarRental.Web.Data;
using CarRental.Web.Models.Domain.CarOffer;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Repositories.CarBDRepo;

public class CarOrderRepository : ICarOrderRepository
{
    private readonly CarDbContext _carDbContext;

    public CarOrderRepository(CarDbContext carDbContext)
    {
        _carDbContext = carDbContext;
    }

    public async Task<IEnumerable<CarOrder>> GetAllAsync()
    {
        return await _carDbContext.CarOrders.ToListAsync();
    }

    public async Task<CarOrder> GetAsync(Guid id)
    {
        return await _carDbContext.CarOrders
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<CarOrder> AddAsync(CarOrder carOrder)
    {
        await _carDbContext.CarOrders.AddAsync(carOrder);
        await _carDbContext.SaveChangesAsync();
        return carOrder;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existingCarOrder = await _carDbContext.CarOrders.FindAsync(id);
        if (existingCarOrder != null)
        {
            _carDbContext.CarOrders.Remove(existingCarOrder);
            await _carDbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
}