namespace CarRental.Web.Models.Domain.CarOffer;

public class CarTag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public Guid CarOfferId { get; set; }
}