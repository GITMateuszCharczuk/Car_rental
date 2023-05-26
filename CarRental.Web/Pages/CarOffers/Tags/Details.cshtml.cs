using CarRental.Web.Models.Domain.Blog;
using CarRental.Web.Models.Domain.CarOffer;
using CarRental.Web.Repositories.CarBDRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages.CarOffers.Tags;

public class Details : PageModel
{
    private readonly ICarOfferRepository _carOfferRepository;

    public Details(ICarOfferRepository carOfferRepository)
    {
        _carOfferRepository = carOfferRepository;
    }

    public List<CarOffer> CarOffers { get; set; }

    public async Task<IActionResult> OnGet(string tagName)
    {
        CarOffers = (await _carOfferRepository.GetAllAsync(tagName)).ToList();
        return Page();
    }
}