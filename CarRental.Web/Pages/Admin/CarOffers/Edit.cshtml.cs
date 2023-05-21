using System.ComponentModel.DataAnnotations;
using CarRental.Web.Enums;
using CarRental.Web.Models.Domain.Blog;
using CarRental.Web.Models.Domain.CarOffer;
using CarRental.Web.Models.ViewModels;
using CarRental.Web.Repositories.BlogRepo;
using CarRental.Web.Repositories.CarBDRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages.Admin.CarOffers;

[Authorize(Roles = "Admin")]
public class Edit : PageModel
{
    private readonly ICarOfferRepository _carOfferRepository;

    public Edit(ICarOfferRepository carOfferRepository)
    {
        _carOfferRepository = carOfferRepository;
    }

    [BindProperty] [Required] public string TagsString { get; set; }
    
    [BindProperty] public string CarGalleryString { get; set; }
    [BindProperty] public EditCarOfferRequest CarOfferRequest { get; set; }

    [BindProperty] public IFormFile FeaturedImage { get; set; }

    public async Task OnGet(Guid id)
    {
        
        var carOfferDomainModel = await _carOfferRepository.GetAsync(id);
        
        if (carOfferDomainModel != null && carOfferDomainModel.Tags != null && carOfferDomainModel.ImageUrls != null && carOfferDomainModel.Tarrifs != null)
        {
            CarOfferRequest = new EditCarOfferRequest
            {
                Id = carOfferDomainModel.Id,
                Heading = carOfferDomainModel.Heading,
                ShortDescription = carOfferDomainModel.ShortDescription,
                FeaturedImageUrl = carOfferDomainModel.FeaturedImageUrl,
                UrlHandle = carOfferDomainModel.Heading,
                Horsepower = carOfferDomainModel.Horsepower,
                YearOfProduction = carOfferDomainModel.YearOfProduction,
                EngineDetails = carOfferDomainModel.EngineDetails,
                DriveDetails = carOfferDomainModel.DriveDetails,
                GearboxDetails = carOfferDomainModel.GearboxDetails,
                CarDeliverylocation = carOfferDomainModel.CarDeliverylocation,
                CarReturnLocation = carOfferDomainModel.CarReturnLocation,
                PublishedDate = carOfferDomainModel.PublishedDate,
                OneNormalDayPrice = carOfferDomainModel.Tarrifs.OneNormalDayPrice,
                OneWeekendDayPrice = carOfferDomainModel.Tarrifs.OneWeekendDayPrice,
                FullWeekendPrice = carOfferDomainModel.Tarrifs.FullWeekendPrice,
                OneWeekPrice = carOfferDomainModel.Tarrifs.OneWeekPrice,
                OneMonthPrice = carOfferDomainModel.Tarrifs.OneMonthPrice,
                Visible = carOfferDomainModel.Visible
            };
                Console.WriteLine(CarOfferRequest.Id);
            TagsString = string.Join(',', carOfferDomainModel.Tags.Select(x => x.Name));
            CarGalleryString = string.Join(',', carOfferDomainModel.ImageUrls.Select(x => x.Url));
        }
    }

    public async Task<IActionResult> OnPost()
    {
        
        ModelState["FeaturedImage"]!.ValidationState = ModelValidationState.Valid;
        // if (ModelState.IsValid){
        Console.WriteLine(CarOfferRequest.Id);
            // try
            // {
                var carOfferDomainModel = new CarOffer
                {
                    Id = CarOfferRequest.Id,
                    Heading = CarOfferRequest.Heading,
                    ShortDescription = CarOfferRequest.ShortDescription,
                    FeaturedImageUrl = CarOfferRequest.FeaturedImageUrl,
                    UrlHandle = CarOfferRequest.UrlHandle,
                    Horsepower = CarOfferRequest.Horsepower,
                    YearOfProduction = CarOfferRequest.YearOfProduction,
                    EngineDetails = CarOfferRequest.EngineDetails,
                    DriveDetails = CarOfferRequest.DriveDetails,
                    GearboxDetails = CarOfferRequest.GearboxDetails,
                    CarDeliverylocation = CarOfferRequest.CarDeliverylocation,
                    CarReturnLocation = CarOfferRequest.CarReturnLocation,
                    PublishedDate = CarOfferRequest.PublishedDate,
                    Visible = CarOfferRequest.Visible,
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
                        OneNormalDayPrice = CarOfferRequest.OneNormalDayPrice,
                        OneWeekendDayPrice = CarOfferRequest.OneWeekendDayPrice,
                        FullWeekendPrice = CarOfferRequest.FullWeekendPrice,
                        OneWeekPrice = CarOfferRequest.OneWeekPrice,
                        OneMonthPrice = CarOfferRequest.OneMonthPrice
                    }
                };

                await _carOfferRepository.UpdateAsync(carOfferDomainModel);

                ViewData["Notification"] = new Notification
                {
                    Message = "Record updated succesfully",
                    Type = NotificationType.Success
                };
                return RedirectToPage("/Admin/CarOffers/List");
            // }
            // catch (Exception ex)
            // {
            //     ViewData["Notification"] = new Notification
            //     {
            //         Message = "Something went wrong",
            //         Type = NotificationType.Error
            //     };
            //     return Page();
            // }
            // }
        ViewData["Notification"] = new Notification
        {
            Message = "Something went wrong",
            Type = NotificationType.Error
        };
        return Page();
    }
}