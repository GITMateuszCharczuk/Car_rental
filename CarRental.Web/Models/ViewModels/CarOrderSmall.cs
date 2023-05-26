namespace CarRental.Web.Models.ViewModels;

public class CarOrderSmall
{
    public string UrlHandle { get; set; }
    public DateTime StartDate{ get; set; }
    public DateTime EndDate{ get; set; }
    public int NumOfDrivers{ get; set; }
}