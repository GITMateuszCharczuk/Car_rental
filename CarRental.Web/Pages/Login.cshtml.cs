using CarRental.Web.Enums;
using CarRental.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages;

public class Login : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public Login(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [BindProperty] public Models.ViewModels.Login LoginViweModel { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(string? ReturnUrl)
    {
        if (ModelState.IsValid)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(
                LoginViweModel.Username, LoginViweModel.Password, false, false);
            if (signInResult.Succeeded)
            {
                if (!string.IsNullOrEmpty(ReturnUrl)) return RedirectToPage(ReturnUrl);

                return RedirectToPage("Index");
            }

            ViewData["Notification"] = new Notification
            {
                Type = NotificationType.Error,
                Message = "Unable to login"
            };

            return Page();
        }

        return Page();
    }
}