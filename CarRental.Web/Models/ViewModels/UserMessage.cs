using System.ComponentModel.DataAnnotations;
using CarRental.Web.Models.Domain.CarOffer;

namespace CarRental.Web.Models.ViewModels;

public class UserMessage
{
    [Required]public string Name { get; set; }

    [Required]public string Surname { get; set; }

    [Required]public int PhoneNumber { get; set; }

    [Required]public string EmailAddress { get; set; }

    [Required]public Guid CarOfferId { get; set; }
    
    [Required]public string Message { get; set; }
}