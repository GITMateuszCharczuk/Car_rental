using CarRental.Web.Enums;

namespace CarRental.Web.Models.ViewModels;

public class Notification
{
    public string Message { get; set; }
    public NotificationType Type { get; set; }
}