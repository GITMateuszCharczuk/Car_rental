﻿using CarRental.Web.Enums;
using CarRental.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages;

public class Register : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;

    public Register(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [BindProperty] public Models.ViewModels.Register RegisterViewModel { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser
            {
                UserName = RegisterViewModel.Username,
                Email = RegisterViewModel.Email
            };
            var identityResult = await _userManager.CreateAsync(user, RegisterViewModel.Password);

            if (identityResult.Succeeded)
            {
                var addRolesResult = await _userManager.AddToRoleAsync(user, "User");
                if (addRolesResult.Succeeded)
                {
                    ViewData["Notification"] = new Notification
                    {
                        Message = "User was registered successfully",
                        Type = NotificationType.Success
                    };

                    return Page();
                }
            }

            ViewData["Notification"] = new Notification
            {
                Message = "Something went wrong",
                Type = NotificationType.Error
            };

            return Page();
        }

        return Page();
    }
}