namespace CarRental.Web.Models.ViewModels;

public class AddCarOrder
{
    public Guid CarOfferId { get; set; }

    public Guid UserId { get; set; }

    public string UserName { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int NumOfDrivers { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public int PhoneNumber { get; set; }

    public string EmailAddress { get; set; }

    public string Address { get; set; }

    public string Postcode { get; set; }

    public string City { get; set; }

    public string DriversLicenseNumber { get; set; }
}