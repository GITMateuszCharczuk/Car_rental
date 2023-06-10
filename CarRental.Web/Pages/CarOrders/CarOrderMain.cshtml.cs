using System.Net;
using System.Net.Mail;
using CarRental.Web.Controllers;
using CarRental.Web.Enums;
using CarRental.Web.Models.Domain.CarOffer;
using CarRental.Web.Models.ViewModels;
using CarRental.Web.Repositories.CarBDRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace CarRental.Web.Pages.CarOrders;

[Authorize(Roles = "User")]
public class CarOrderMain : PageModel
{
    private readonly ICarOfferRepository _carOfferRepository;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ICarOrderRepository _carOrderRepository;
    private readonly ISession _session;
    private readonly EmailSender _emailSender;
    public CarOffer CarOffer { get; set; }

    [BindProperty] public AddCarOrder CarOrder { get; set; }

    public string SpanValue { get; set; }

    public CarOrderMain(ICarOfferRepository carOfferRepository,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        ICarOrderRepository carOrderRepository,
        IHttpContextAccessor httpContextAccessor,
        EmailSender emailSender
    )
    {
        _emailSender = emailSender;
        _carOrderRepository = carOrderRepository;
        _carOfferRepository = carOfferRepository;
        _signInManager = signInManager;
        _userManager = userManager;
        _session = httpContextAccessor.HttpContext.Session;
    }


    public async Task<PageResult> OnGetAsync(string urlHandle)
    {
        CarOffer = await _carOfferRepository.GetAsync(urlHandle);
        var notificationJson = (string)TempData["Notification"];
        var smallOrderJson = (string)TempData["CarOrderSmall"];
        // var modelStateJson = _session.GetString("ModelState");
        var addCarOrderJson = _session.GetString("AddCarOrder");
        // if (!string.IsNullOrEmpty(modelStateJson))
        // {
        //     var deserializedModelState = DeserializeModelState(modelStateJson);
        //     var newModelState = new ModelStateDictionary();
        //
        //     foreach (var keyValuePair in deserializedModelState)
        //     {
        //         newModelState.SetModelValue(keyValuePair.Value);
        //     }
        //     ModelState.Merge(deserializedModelState);
        //     _session.Remove("ModelState");
        // }

        if (notificationJson != null)
        {
            ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
        }

        if (!string.IsNullOrEmpty(addCarOrderJson))
        {
            CarOrder = JsonConvert.DeserializeObject<AddCarOrder>(addCarOrderJson)!;
            
        }else if (smallOrderJson != null)
        {
            CarOrderSmall carOrderSmall = JsonSerializer.Deserialize<CarOrderSmall>(smallOrderJson)!;
            TempData["CarOrderSmall"] = JsonSerializer.Serialize(carOrderSmall);
            if (carOrderSmall != null)
            {
                CarOrder = new AddCarOrder
                {
                    StartDate = carOrderSmall.StartDate,
                    EndDate = carOrderSmall.EndDate,
                    NumOfDrivers = carOrderSmall.NumOfDrivers,
                };
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPost(string urlHandle)
    {
        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        var totalPrice = int.Parse(Request.Form["spanTotalPriceValue"].ToString());
        var carOfferId = Guid.Parse(Request.Form["carOfferIdField"].ToString());
        var result = ValidateAddOrder(urlHandle);
        
        if (result != null)
        {
            return result;
        }
        
        if (currentUser != null && _signInManager.IsSignedIn(User))
        {
            if (ModelState.IsValid)
            {
                var carOrder = new CarOrder
                {
                    UserId = Guid.Parse(_userManager.GetUserId(User) ?? throw new InvalidOperationException()),
                    CarOfferId = carOfferId,
                    UserName = currentUser.UserName!,
                    StartDate = CarOrder.StartDate,
                    EndDate = CarOrder.EndDate,
                    NumOfDrivers = CarOrder.NumOfDrivers,
                    Name = CarOrder.Name,
                    Surname = CarOrder.Surname,
                    PhoneNumber = CarOrder.PhoneNumber,
                    EmailAddress = CarOrder.EmailAddress,
                    Address = CarOrder.Address,
                    Postcode = CarOrder.Postcode,
                    City = CarOrder.City,
                    DriversLicenseNumber = CarOrder.DriversLicenseNumber,
                    TotalPrice = totalPrice,
                    State = "Active"
                };

                await _carOrderRepository.AddAsync(carOrder);
                await OnPostSendEmailAsync();
                var notification = new Notification
                {
                    Message = "Our order was successful, wait for Email.",
                    Type = NotificationType.Success
                };
                
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/CarOffers/CarOffersMain");
            }
        }

        return RedirectWithError(urlHandle,"Something went wrong, try again")!;
    }

    private IActionResult? RedirectWithError(string urlHandle, string message)
    {
        var notificationError = new Notification
        {
            Message = message,
            Type = NotificationType.Error
        };
        _session.SetString("AddCarOrder", JsonSerializer.Serialize(CarOrder));
        TempData["Notification"] = JsonSerializer.Serialize(notificationError);
        
        return RedirectToPage("/CarOrders/CarOrderMain", new { urlHandle });
    }

    private IActionResult? ValidateAddOrder(string urlHandle)
    {
        if (CarOrder.StartDate.Date.CompareTo(CarOrder.EndDate.Date) >= 0)
        {
            return RedirectWithError(urlHandle,"End date must be past start date");
        }
        if (CarOrder.StartDate.Date.CompareTo(DateTime.Now.Date) < 0)
        {
            return RedirectWithError(urlHandle,"Start date can only be today's date or future date.");
        }
        if (CarOrder.EndDate.Date.CompareTo(DateTime.Now.Date.AddDays(1)) < 0)
        {
            return RedirectWithError(urlHandle,"End date can only be a future date.");
        }

        return null;
    }

    public async Task OnPostSendEmailAsync()
    {
        string email = "mateusz.charczuk10@gmail.com";
        string subject = "Hello from SendGrid";
        string message = "This is the email message.";

        if (await _emailSender.SendEmailAsync(email, subject, message))
        {
            
        }
        else
        {
            Console.WriteLine("asdfsdjfpsdnmasdfasfsdfsdfasdfg");
        }
    }
}