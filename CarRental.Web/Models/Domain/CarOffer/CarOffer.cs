namespace CarRental.Web.Models.Domain.CarOffer;

public class CarOffer
{
    public Guid Id { get; set; }
    public string Heading { get; set; }
    public string ShortDescription { get; set; }
    public string FeaturedImageUrl { get; set; }
    public string UrlHandle { get; set; }
    public string Horsepower { get; set; }
    public int YearOfProduction { get; set; }
    public string EngineDetails { get; set; }
    public string DriveDetails { get; set; }
    public string GearboxDetails { get; set; }
    public string CarDeliverylocation { get; set; }
    public string CarReturnLocation { get; set; }
    public DateTime PublishedDate { get; set; }
    public bool Visible { get; set; }
    
    // Navigation Property
    public ICollection<CarTag> Tags { get; set; }
    public ICollection<ImageUrl> ImageUrls { get; set; }
    public Tarrif Tarrifs { get; set; }
}