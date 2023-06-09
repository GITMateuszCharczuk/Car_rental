﻿using System.Text.Json;
using CarRental.Web.Enums;
using CarRental.Web.Models.Domain;
using CarRental.Web.Models.Domain.Blog;
using CarRental.Web.Models.ViewModels;
using CarRental.Web.Repositories;
using CarRental.Web.Repositories.BlogRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages.Admin.Blogs;

[Authorize(Roles = "Admin")]
public class List : PageModel
{
    private readonly IBlogPostRepository _blogPostRepository;

    public List(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    public List<BlogPost>? BlogPosts { get; set; }

    public async Task OnGetAsync()
    {
        var notificationJson = (string)TempData["Notification"];
        if (notificationJson != null)
            ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);

        BlogPosts = (await _blogPostRepository.GetAllAsync())?.ToList();
    }

    public async Task OnGetDeleteAsync(Guid id)
    {
        try
        {
            await _blogPostRepository.DeleteAsync(id);
            BlogPosts = (await _blogPostRepository.GetAllAsync())?.ToList();

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
            throw;
        }
    }
}