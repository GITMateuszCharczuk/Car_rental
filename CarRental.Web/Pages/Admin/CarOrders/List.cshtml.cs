using System.Text.Json;
using CarRental.Web.Enums;
using CarRental.Web.Models.Domain.CarOffer;
using CarRental.Web.Models.ViewModels;
using CarRental.Web.Repositories.CarBDRepo;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages.Admin.CarOrders;

public class List : PageModel
{
    private readonly ICarOrderRepository _carOrderRepository;
    private readonly ICarOfferRepository _carOfferRepository;

    public List(ICarOrderRepository carOrderRepository, ICarOfferRepository carOfferRepository)
    {
        _carOfferRepository = carOfferRepository;
        _carOrderRepository = carOrderRepository;
    }

    public List<CarOrder>? CarOrders { get; set; }
    
    public List<CarOffer> CarOffers { get; set; }

    public async Task<PageResult> OnGetAsync()
    {
        var notificationJson = (string)TempData["Notification"]!;
        if (notificationJson != null)
        {
            ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
        }

        CarOffers = (await _carOfferRepository.GetAllAsync()).ToList();
        var carOrdersTemp = (await _carOrderRepository.GetAllAsync()).ToList();

        int numOfDeletedOffers = 0;
        var deleteAsyncResult = true;
        foreach (var carOrder in carOrdersTemp)
        {
            if (carOrder.EndDate < DateTime.Today || carOrder.State.Equals("Expired"))
            {
                try
                {
                    var result = (await _carOrderRepository.DeleteAsync(carOrder.Id));
                    deleteAsyncResult = result && deleteAsyncResult;
                    numOfDeletedOffers++;
                }
                catch (Exception e)
                {
                    CarOrders = (await _carOrderRepository.GetAllAsync()).ToList();
                    SendErrorMessage();
                    return Page();
                }
            }
        }

        if (deleteAsyncResult)
        {
            if (numOfDeletedOffers > 0)
            {
                ViewData["Notification2"]= new Notification
                {
                    Message = numOfDeletedOffers+" Expired orders deleted!!!",
                    Type = NotificationType.Info
                };
            }
        }
        else
        {
            CarOrders = (await _carOrderRepository.GetAllAsync()).ToList();
            SendErrorMessage();
        }
        CarOrders = (await _carOrderRepository.GetAllAsync()).ToList();
        return Page();
    }

    public async Task<PageResult> OnGetDeleteAsync(Guid id)
    {
        try
        {
            await _carOrderRepository.DeleteAsync(id);
            CarOrders = (await _carOrderRepository.GetAllAsync()).ToList();
            CarOffers = (await _carOfferRepository.GetAllAsync()).ToList();
            ViewData["Notification"] = new Notification
            {
                Message = "Record deleted successfully",
                Type = NotificationType.Success
            };
        }
        catch (Exception e)
        {
            ViewData["Notification"] = new Notification
            {
                Message = "Something went wrong",
                Type = NotificationType.Error
            };
        }
        return Page();
    }
    
    void SendErrorMessage()
    {
        ViewData["Notification2"] = new Notification
        {
            Message = "Something went wrong, while deleting old offers!!!",
            Type = NotificationType.Error
        };
    }
}