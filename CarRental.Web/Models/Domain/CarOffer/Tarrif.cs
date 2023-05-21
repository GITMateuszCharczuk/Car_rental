namespace CarRental.Web.Models.Domain.CarOffer;

public class Tarrif
{
    public Guid Id { get; set; }
    public Guid CarOfferId { get; set; }
    public Double OneNormalDayPrice { get; set; }
    public Double OneWeekendDayPrice { get; set; }
    public Double FullWeekendPrice { get; set; }
    public Double OneWeekPrice { get; set; }
    public Double OneMonthPrice { get; set; }
}