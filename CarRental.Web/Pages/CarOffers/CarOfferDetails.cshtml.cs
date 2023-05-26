using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using CarRental.Web.Models.Domain.CarOffer;
using CarRental.Web.Models.ViewModels;
using CarRental.Web.Repositories.CarBDRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace CarRental.Web.Pages.CarOffers;

public class CarOfferDetails : PageModel
{
    private readonly ICarOfferRepository _carOfferRepository;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public CarOfferDetails(ICarOfferRepository carOfferRepository,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _carOfferRepository = carOfferRepository;
    }

    public CarOffer CarOffer { get; set; }

    [BindProperty] public CarOrderSmall CarOrderSmall { get; set; }

    [BindProperty] public Guid CarOfferId { get; set; }

    public async Task<IActionResult> OnGet(string urlHandle)
    {
        CarOffer = await _carOfferRepository.GetAsync(urlHandle);
        if (CarOffer != null)
        {
            CarOfferId = CarOffer.Id;

        }

        return Page();
    }

    public Task<IActionResult> OnPost(string urlHandle)
    {
        if (_signInManager.IsSignedIn(User))
        {
            CarOrderSmall.UrlHandle = urlHandle;
            TempData["CarOrderSmall"] = JsonSerializer.Serialize(CarOrderSmall);
        }

        return Task.FromResult<IActionResult>(RedirectToPage("/CarOffers/CarOrderMain"));
    }
}