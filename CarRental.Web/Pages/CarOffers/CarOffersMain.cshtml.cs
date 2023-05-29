using System.Text.Json;
using CarRental.Web.Models.Domain.CarOffer;
using CarRental.Web.Models.ViewModels;
using CarRental.Web.Repositories.CarBDRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace CarRental.Web.Pages.CarOffers;

public class CarOffersMain : PageModel
{
    private readonly ICarOfferRepository _carOfferRepository;
    private readonly ICarTagRepository _tagRepository;

    public CarOffersMain( ICarOfferRepository carOfferRepository, ICarTagRepository tagRepository)
    {
        _tagRepository = tagRepository;
        _carOfferRepository = carOfferRepository;
    }

    [BindProperty] public List<CarOffer> CarOffers { get; set; }
    [BindProperty] public List<CarTag> Tags { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var notificationJson = (string)TempData["Notification"];
        if (notificationJson != null)
        {
            ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
        }

        CarOffers = (await _carOfferRepository.GetAllAsync()).ToList();
        Tags = (await _tagRepository.GetAllAsync()).ToList();
        return Page();
    }
}