using System.ComponentModel.DataAnnotations;

namespace CarRental.Web.Models.ViewModels;

public class EditCarOfferRequest
{
    [Required] public Guid Id { get; set; }
    [Required] public string Heading { get; set; }

    [Required]public string ShortDescription { get; set; }
    
    public string FeaturedImageUrl { get; set; }
    
    [Required] public string UrlHandle { get; set; }

    [Required] public string Horsepower { get; set; }

    [Required] public int YearOfProduction { get; set; }

    [Required] public string EngineDetails { get; set; }

    [Required] public string DriveDetails { get; set; }

    [Required] public string GearboxDetails { get; set; }

    [Required] public string CarDeliverylocation { get; set; }

    [Required] public string CarReturnLocation { get; set; }
    
    [Required] public DateTime PublishedDate { get; set; }
    
    [Required] public Double OneNormalDayPrice { get; set; }
    
    [Required] public Double OneWeekendDayPrice { get; set; }
    
    [Required] public Double FullWeekendPrice { get; set; }
    
    [Required]public Double OneWeekPrice { get; set; }
    
    [Required] public Double OneMonthPrice { get; set; }
    
    public bool Visible { get; set; }
}