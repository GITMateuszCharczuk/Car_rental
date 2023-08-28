using System.Text.Json;
using CarRental.Web.Enums;
using CarRental.Web.Models.Domain.CarOffer;
using CarRental.Web.Models.ViewModels;
using CarRental.Web.Repositories.CarBDRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages;

public class Contact : PageModel
{
    private readonly ICarOfferRepository _carOfferRepository;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    [BindProperty] public UserMessage UserMessage { set; get; } = new UserMessage();
    [BindProperty] public List<CarOffer> CarOffers { get; set; }

    public Contact(ICarOfferRepository carOfferRepository,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _carOfferRepository = carOfferRepository;
    }

    public async Task OnGet()
    {
        // CarOffers = (await _carOfferRepository.GetAllAsync()).ToList();
        
        var notificationJson = (string)TempData["Notification"]!;
        if (notificationJson != null)
            ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
        
        
    }

    public async Task<IActionResult> OnPost()
    {
        if (_signInManager.IsSignedIn(User))
        {
            if (ModelState.IsValid)
            {
                var userMessage = new UserMessage
                {
                    CarOfferId = UserMessage.CarOfferId,
                    Name = UserMessage.Name,
                    Surname = UserMessage.Surname,
                    PhoneNumber = UserMessage.PhoneNumber,
                    EmailAddress = UserMessage.EmailAddress,
                    Message = UserMessage.Message
                };
                Console.WriteLine(UserMessage.CarOfferId+
                    " " + UserMessage.Name +
                    " " +  UserMessage.Surname +
                    " "+   UserMessage.PhoneNumber +
                    " "+ UserMessage.EmailAddress +
                    "" + UserMessage.Message);
                var notification = new Notification
                {
                    Message = "Message have been send!!!",
                    Type = NotificationType.Success
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/Contact");
                // return Page();
            }
        }

        var notification2 = new Notification
        {
            Message = "User isn't signed in!!",
            Type = NotificationType.Error
        };

        TempData["Notification"] = JsonSerializer.Serialize(notification2);
        // return RedirectToPage("/Contact");
        var notificationJson = (string)TempData["Notification"]!;
        ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
        return Page();
    }
    
    public async Task<IActionResult> OnGetCarOffersAsync()
    {
        CarOffers = (await _carOfferRepository.GetAllAsync()).ToList();
        return new JsonResult(CarOffers);
    }
}