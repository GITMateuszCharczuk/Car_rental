using System.Text.Json;
using CarRental.Web.Models.Domain.CarOffer;
using CarRental.Web.Models.ViewModels;
using CarRental.Web.Repositories.CarBDRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages.CarOffers;
[Authorize(Roles = "User")]
public class CarOrderMain : PageModel
{
    private readonly ICarOfferRepository _carOfferRepository;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    
    public CarOrderMain(ICarOfferRepository carOfferRepository,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    {
        _carOfferRepository = carOfferRepository;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [BindProperty] public CarOffer CarOffer { get; set; }
    
    [BindProperty] public AddCarOrder CarOrder { get; set; }
    
    public async Task OnGetAsync()
    {
        var notificationJson = (string)TempData["CarOrderSmall"];
        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        CarOrderSmall? carOrderSmall = null;
        
        if (notificationJson != null)
        {
            carOrderSmall = JsonSerializer.Deserialize<CarOrderSmall>(notificationJson);
        }
        

        if (carOrderSmall != null && currentUser != null && _signInManager.IsSignedIn(User))
        {
            CarOffer = await _carOfferRepository.GetAsync(carOrderSmall.UrlHandle);
            CarOrder = new AddCarOrder
            {
                CarOfferId = CarOffer.Id,
                StartDate = carOrderSmall.StartDate,
                UserId = Guid.Parse(_userManager.GetUserId(User) ?? throw new InvalidOperationException()),
                EndDate = carOrderSmall.EndDate,
                NumOfDrivers = carOrderSmall.NumOfDrivers,
                UserName = currentUser.UserName!
            };
        }
        
    }
}