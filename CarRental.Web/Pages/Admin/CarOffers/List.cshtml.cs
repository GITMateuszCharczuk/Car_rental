using System.Text.Json;
using CarRental.Web.Enums;
using CarRental.Web.Models.Domain;
using CarRental.Web.Models.Domain.CarOffer;
using CarRental.Web.Models.ViewModels;
using CarRental.Web.Repositories.BlogRepo;
using CarRental.Web.Repositories.CarBDRepo;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages.Admin.CarOffers;

public class List : PageModel
{
    private readonly ICarOfferRepository _carOfferRepository;

    public List(ICarOfferRepository carOfferRepository)
    {
        _carOfferRepository = carOfferRepository;
    }

    public List<CarOffer>? CarOffers { get; set; }

    public async Task OnGetAsync()
    {
        var notificationJson = (string)TempData["Notification"]!;
        if (notificationJson != null)
            ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);

        CarOffers = (await _carOfferRepository.GetAllAsync()).ToList();
    }

    public async Task OnGetDeleteAsync(Guid id)
    {
        try
        {
            await _carOfferRepository.DeleteAsync(id);
            CarOffers = (await _carOfferRepository.GetAllAsync())?.ToList();

            ViewData["Notification"] = new Notification
            {
                Message = "Record deleted successfully",
                Type = NotificationType.Success
            };
        }
        catch (Exception e)
        {
            ViewData["Notification"] = new Notification
            {
                Message = "Something went wrong",
                Type = NotificationType.Error
            };
        }
    }
}