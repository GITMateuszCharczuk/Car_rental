using CarRental.Web.Models.Domain;
using CarRental.Web.Models.Domain.CarOffer;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Data;

public class CarDbContext : DbContext
{

    public CarDbContext(DbContextOptions<CarDbContext> options) : base(options)
    {
    }

    public DbSet<CarTag> CarTags { get; set; }
    public DbSet<CarOrder> CarOrders { get; set; }
    
    public DbSet<Tarrif> Tarrifs { get; set; }
    public DbSet<ImageUrl> ImageUrls { get; set; }
    public DbSet<CarOffer> CarOffers { get; set; }
}