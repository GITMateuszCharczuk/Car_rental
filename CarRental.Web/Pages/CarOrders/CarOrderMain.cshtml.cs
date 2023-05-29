using System.Net;
using System.Net.Mail;
using System.Text.Json;
using CarRental.Web.Enums;
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
    private readonly ICarOrderRepository _carOrderRepository;
    
    public CarOffer CarOffer { get; set; }
    
    [BindProperty] public AddCarOrder CarOrder { get; set; }
    
    public string SpanValue { get; set; }
    
    public CarOrderMain(ICarOfferRepository carOfferRepository,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        ICarOrderRepository carOrderRepository)
    {
        _carOrderRepository = carOrderRepository;
        _carOfferRepository = carOfferRepository;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    
    
    public async Task<PageResult> OnGetAsync(string urlHandle)
    {
        
        var notificationJson = (string)TempData["Notification"];
        if (notificationJson != null)
        {
            ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
        }
        CarOffer = await _carOfferRepository.GetAsync(urlHandle);
        
        var smallOrderJson = (string)TempData["CarOrderSmall"];
        CarOrderSmall? carOrderSmall = null;
        
        if (smallOrderJson != null)
        {
            carOrderSmall = JsonSerializer.Deserialize<CarOrderSmall>(smallOrderJson);
            TempData["CarOrderSmall"] = JsonSerializer.Serialize(carOrderSmall);
        }
        

        if (carOrderSmall != null)
        {
            CarOrder = new AddCarOrder
            {
                StartDate = carOrderSmall.StartDate,
                EndDate = carOrderSmall.EndDate,
                NumOfDrivers = carOrderSmall.NumOfDrivers,
            };
        }
        return Page();

    }

    public async Task<IActionResult> OnPost(string urlHandle)
    {
        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        
        var totalPrice = int.Parse(Request.Form["spanTotalPriceValue"].ToString());
        var carOfferId = Guid.Parse(Request.Form["carOfferIdField"].ToString());
        // ValidateAddOrder();
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
                
                var notification = new Notification
                {
                    Message = "Our order was successful, wait for Email.",
                    Type = NotificationType.Success
                };
                
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/CarOffers/CarOffersMain");
            }
        } 
        var notificationError = new Notification
        {
            Message = "Something went wrong, try again",
            Type = NotificationType.Error
        };
                
        TempData["Notification"] = JsonSerializer.Serialize(notificationError);
        return RedirectToPage("/CarOrders/CarOrderMain", new { urlHandle });
    }
    
    private void ValidateAddOrder()
    {
        if (CarOrder.StartDate.Date > CarOrder.EndDate.Date)
        {
            ModelState.AddModelError("CarOrder.StartDate",
                $"{nameof(CarOrder.StartDate)} end date must be past start date");
        } else if (CarOrder.StartDate.Date < DateTime.Now.Date)
        {
            ModelState.AddModelError("CarOrder.StartDate",
                $"{nameof(CarOrder.StartDate)} can only be today's date or future date.");
        }else if (CarOrder.EndDate.Date > DateTime.Now.Date.AddDays(1))
        {
            ModelState.AddModelError("CarOrder.EndDate",
                $"{nameof(CarOrder.EndDate)} can only be future date.");
        }
    }
    
    public async Task SendEmailAsync(string recipientEmail, string subject, string message)
    {
        var senderEmail = "your-email@example.com"; 
        var senderPassword = "your-email-password"; 

        var smtpClient = new SmtpClient("smtp.example.com", 587); // Replace with your SMTP server details
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

        var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(senderEmail);
        mailMessage.To.Add(new MailAddress(recipientEmail));
        mailMessage.Subject = subject;
        mailMessage.Body = message;

        await smtpClient.SendMailAsync(mailMessage);
    }
}