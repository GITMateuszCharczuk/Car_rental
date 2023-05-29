using System.ComponentModel.DataAnnotations;

namespace CarRental.Web.Models.ViewModels;

public class AddCarOrder
{
    [Required]public DateTime StartDate { get; set; }

    [Required]public DateTime EndDate { get; set; }

    [Required]public int NumOfDrivers { get; set; }

    [Required]public string Name { get; set; }

    [Required]public string Surname { get; set; }

    [Required]public int PhoneNumber { get; set; }

    [Required]public string EmailAddress { get; set; }

    [Required]public string Address { get; set; }

    [Required]public string Postcode { get; set; }

    [Required]public string City { get; set; }

    [Required]public string DriversLicenseNumber { get; set; }
}