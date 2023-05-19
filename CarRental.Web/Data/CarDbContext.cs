using CarRental.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Data;

public class CarDbContext : DbContext
{

    public CarDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }

    public DbSet<CarTag> CarTags { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Tarrif> Tarrifs { get; set; }//chyba do cipy
    public DbSet<ImageUrl> ImageUrls { get; set; }//chyba do cipy
    public DbSet<CarOffer> CarOffers { get; set; }
}