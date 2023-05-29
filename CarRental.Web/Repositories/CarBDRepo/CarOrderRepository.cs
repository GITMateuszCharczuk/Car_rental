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
    
    public async Task<CarOrder> UpdateAsync(CarOrder carOrder)
    {
        var existingCarOrder = await _carDbContext.CarOrders
            .FirstOrDefaultAsync(x => x.Id == carOrder.Id);
        
        if (existingCarOrder != null)
        {
            existingCarOrder.Id = carOrder.Id;
            existingCarOrder.Id = carOrder.Id;
            existingCarOrder.UserId = carOrder.UserId;
            existingCarOrder.CarOfferId = carOrder.CarOfferId;
            existingCarOrder.UserName = carOrder.UserName;
            existingCarOrder.StartDate = carOrder.StartDate;
            existingCarOrder.EndDate = carOrder.EndDate;
            existingCarOrder.NumOfDrivers = carOrder.NumOfDrivers;
            existingCarOrder.Name = carOrder.Name;
            existingCarOrder.Surname = carOrder.Surname;
            existingCarOrder.PhoneNumber = carOrder.PhoneNumber;
            existingCarOrder.EmailAddress = carOrder.EmailAddress;
            existingCarOrder.Address = carOrder.Address;
            existingCarOrder.Postcode = carOrder.Postcode;
            existingCarOrder.City = carOrder.City;
            existingCarOrder.DriversLicenseNumber = carOrder.DriversLicenseNumber;
            existingCarOrder.TotalPrice = carOrder.TotalPrice;
            existingCarOrder.State = carOrder.State;
        }

        await _carDbContext.SaveChangesAsync();
        return existingCarOrder;
    }
}