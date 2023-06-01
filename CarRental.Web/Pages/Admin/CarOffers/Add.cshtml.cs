using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using CarRental.Web.Enums;
using CarRental.Web.Models.Domain;
using CarRental.Web.Models.Domain.Blog;
using CarRental.Web.Models.Domain.CarOffer;
using CarRental.Web.Models.ViewModels;
using CarRental.Web.Repositories.CarBDRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages.Admin.CarOffers;

[Authorize(Roles = "Admin")]
public class Add : PageModel
{
    private readonly ICarOfferRepository _carOfferRepository;

    public Add(ICarOfferRepository carOfferRepository)
    {
        _carOfferRepository = carOfferRepository;
    }

    [BindProperty] public IFormFile FeaturedImage { get; set; }

    [BindProperty] public string CarGalleryString { get; set; }
    [BindProperty] [Required] public string TagsString { get; set; }
    [BindProperty] public AddCarOffer AddCarOfferRequest { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        ModelState["FeaturedImage"]!.ValidationState = ModelValidationState.Valid;
        if (ModelState.IsValid)
        {
            var carOffer = new CarOffer
            {
                Heading = AddCarOfferRequest.Heading,
                ShortDescription = AddCarOfferRequest.ShortDescription,
                FeaturedImageUrl = AddCarOfferRequest.FeaturedImageUrl,
                UrlHandle = AddCarOfferRequest.UrlHandle,
                Horsepower = AddCarOfferRequest.Horsepower,
                YearOfProduction = AddCarOfferRequest.YearOfProduction,
                EngineDetails = AddCarOfferRequest.EngineDetails,
                DriveDetails = AddCarOfferRequest.DriveDetails,
                GearboxDetails = AddCarOfferRequest.GearboxDetails,
                CarDeliverylocation = AddCarOfferRequest.CarDeliverylocation,
                CarReturnLocation = AddCarOfferRequest.CarReturnLocation,
                PublishedDate = AddCarOfferRequest.PublishedDate,
                Visible = AddCarOfferRequest.Visible,
                Tags = new List<CarTag>(TagsString.Split(',').Select(x => new CarTag
                {
                    Name = x.Trim()
                })),
                ImageUrls = new List<ImageUrl>(CarGalleryString.Split(',').Select(x => new ImageUrl
                {
                    Url = x.Trim()
                })),
                Tarrifs = new Tarrif
                {
                    OneNormalDayPrice = AddCarOfferRequest.OneNormalDayPrice,
                    OneWeekendDayPrice = AddCarOfferRequest.OneWeekendDayPrice,
                    FullWeekendPrice = AddCarOfferRequest.FullWeekendPrice,
                    OneWeekPrice = AddCarOfferRequest.OneWeekPrice,
                    OneMonthPrice = AddCarOfferRequest.OneMonthPrice
                }
            };
            
            await _carOfferRepository.AddAsync(carOffer);

            var notification = new Notification
            {
                Message = "New Car Offer created!",
                Type = NotificationType.Success
            };
            TempData["Notification"] = JsonSerializer.Serialize(notification);
            return RedirectToPage("/Admin/CarOffers/List");
        }

        return Page();
    }
}