namespace CarRental.Web.Models.Domain.CarOffer;

public class ImageUrl
{
    public Guid Id { get; set; }
    public Guid CarOfferId { get; set; }
    public string Url { get; set; }
}