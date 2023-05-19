namespace CarRental.Web.Models.Domain;

public class Tarrif
{
    public Guid Id { get; set; }
    public Guid CarOfferId { get; set; }
    public string Duration { get; set; }
    public Decimal Price { get; set; }
}