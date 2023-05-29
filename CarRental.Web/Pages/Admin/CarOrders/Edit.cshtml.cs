using System.ComponentModel.DataAnnotations;
using CarRental.Web.Enums;
using CarRental.Web.Models.Domain.CarOffer;
using CarRental.Web.Models.ViewModels;
using CarRental.Web.Repositories.CarBDRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages.Admin.CarOrders;

[Authorize(Roles = "Admin")]
public class Edit : PageModel
{
    private readonly ICarOrderRepository _carOrderRepository;

    public Edit(ICarOrderRepository carOrderRepository)
    {
        _carOrderRepository = carOrderRepository;
    }

    [BindProperty] public EditCarOrderRequest CarOrderRequest { get; set; }

    public async Task OnGet(Guid id)
    {
        var carOrderDomainModel = await _carOrderRepository.GetAsync(id);

        if (carOrderDomainModel != null)
        {
            CarOrderRequest = new EditCarOrderRequest
            {
                Id = carOrderDomainModel.Id,
                UserId = carOrderDomainModel.UserId,
                CarOfferId = carOrderDomainModel.CarOfferId,
                UserName = carOrderDomainModel.UserName,
                StartDate = carOrderDomainModel.StartDate,
                EndDate = carOrderDomainModel.EndDate,
                NumOfDrivers = carOrderDomainModel.NumOfDrivers,
                Name = carOrderDomainModel.Name,
                Surname = carOrderDomainModel.Surname,
                PhoneNumber = carOrderDomainModel.PhoneNumber,
                EmailAddress = carOrderDomainModel.EmailAddress,
                Address = carOrderDomainModel.Address,
                Postcode = carOrderDomainModel.Postcode,
                City = carOrderDomainModel.City,
                DriversLicenseNumber = carOrderDomainModel.DriversLicenseNumber,
                TotalPrice = carOrderDomainModel.TotalPrice,
                State = carOrderDomainModel.State
            };
        }
    }

    public async Task<IActionResult> OnPost()
    {
        try
        {
            var carOrder = new CarOrder
            {
                Id = CarOrderRequest.Id,
                UserId = CarOrderRequest.UserId,
                CarOfferId = CarOrderRequest.CarOfferId,
                UserName = CarOrderRequest.UserName,
                StartDate = CarOrderRequest.StartDate,
                EndDate = CarOrderRequest.EndDate,
                NumOfDrivers = CarOrderRequest.NumOfDrivers,
                Name = CarOrderRequest.Name,
                Surname = CarOrderRequest.Surname,
                PhoneNumber = CarOrderRequest.PhoneNumber,
                EmailAddress = CarOrderRequest.EmailAddress,
                Address = CarOrderRequest.Address,
                Postcode = CarOrderRequest.Postcode,
                City = CarOrderRequest.City,
                DriversLicenseNumber = CarOrderRequest.DriversLicenseNumber,
                TotalPrice = CarOrderRequest.TotalPrice,
                State = CarOrderRequest.State
            };

            await _carOrderRepository.UpdateAsync(carOrder);

            ViewData["Notification"] = new Notification
            {
                Message = "Record updated succesfully",
                Type = NotificationType.Success
            };
            return RedirectToPage("/Admin/CarOrders/List");
        }
        catch (Exception ex)
        {
            ViewData["Notification"] = new Notification
            {
                Message = "Something went wrong",
                Type = NotificationType.Error
            };
            return Page();
        }
    }
}
