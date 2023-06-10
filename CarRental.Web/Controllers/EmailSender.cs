using SendGrid;
using SendGrid.Helpers.Mail;

namespace CarRental.Web.Controllers;

public class EmailSender
{
    private readonly string _sendGridApiKey;

    public EmailSender(IConfiguration configuration)
    {
        _sendGridApiKey = configuration.GetSection("SendGrid")["ApiKey"];
    }

    public async Task<bool> SendEmailAsync(string email, string subject, string message)
    {
        Console.WriteLine(_sendGridApiKey);
        var client = new SendGridClient(_sendGridApiKey);
        var from = new EmailAddress("CarRentalBussines@gmail.pl", "CEO"); // Update with your own email address and name
        var to = new EmailAddress(email);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
        return (await client.SendEmailAsync(msg)).IsSuccessStatusCode;
    }
}