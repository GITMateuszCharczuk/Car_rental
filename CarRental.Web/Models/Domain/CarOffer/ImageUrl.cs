namespace CarRental.Web.Models.Domain;

public class ImageUrl
{
    public Guid Id { get; set; }
    public Guid CarId { get; set; }
    public string Url { get; set; }
}