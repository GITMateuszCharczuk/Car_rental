using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages;

public class Logout : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public Logout(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<IActionResult> OnGet()
    {
        await _signInManager.SignOutAsync();
        return RedirectToPage("Index");
    }
}