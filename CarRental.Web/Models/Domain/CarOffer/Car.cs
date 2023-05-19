namespace CarRental.Web.Models.Domain;

public class Car
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public int YearOfProduction { get; set; }
    public string EngineDetails { get; set; }
    public string DriveDetails { get; set; }
    public string GearboxDetails { get; set; }
}