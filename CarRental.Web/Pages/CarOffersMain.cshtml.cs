using CarRental.Web.Models.Domain.CarOffer;
using CarRental.Web.Repositories.CarBDRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages;

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
        CarOffers = (await _carOfferRepository.GetAllAsync()).ToList();
        Tags = (await _tagRepository.GetAllAsync()).ToList();
        return Page();
    }
}