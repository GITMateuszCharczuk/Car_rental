namespace CarRental.Web.Models.Domain.CarOffer;

public class CarOrder
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    
    public Guid CarOfferId { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public int NumOfDrivers { get; set; }
}