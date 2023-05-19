namespace CarRental.Web.Models.Domain;

public class CarOffer
{
    public Guid Id { get; set; }
    
    public Car car { get; set; }
    
    public string Heading { get; set; }

    public string PageTitle { get; set; }

    public string ShortDescription { get; set; }

    public string FeaturedImageUrl { get; set; }

    public string UrlHandle { get; set; }

    public string miejsceWynajecia { get; set; }

    public string miejsceOddania { get; set; }
    
    public bool Visible { get; set; }

    public DateTime StartRentDate { get; set; }
    
    public DateTime EndRentDate { get; set; }
    
    public ICollection<ImageUrl> ImageUrls { get; set; }
    
    public ICollection<CarTag> CarTags { get; set; }
    
    public ICollection<Tarrif> Tarrifs { get; set; }
    
    
}