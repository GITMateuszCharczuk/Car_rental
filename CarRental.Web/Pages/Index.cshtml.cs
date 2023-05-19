using CarRental.Web.Models.Domain;
using CarRental.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages;

public class IndexModel : PageModel
{

    private readonly ILogger<IndexModel> _logger;


    public IndexModel(ILogger<IndexModel> logger)
    {

        _logger = logger;
    }
    

    public async Task<IActionResult> OnGet()
    {
        return Page();
    }
}