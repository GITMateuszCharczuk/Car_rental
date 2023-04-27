using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Bloggie.Web.Enums;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.Blogs;

[Authorize(Roles = "Admin")]
public class AddModel : PageModel
{
    private readonly IBlogPostRepository _blogPostRepository;

    public AddModel(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    [BindProperty] public IFormFile FeaturedImage { get; set; }

    [BindProperty][Required] public string Tags { get; set; }
    [BindProperty] public AddBlogPost AddBlogPostRequest { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        ValidateAddBlogPost();
        if (ModelState.IsValid)
        {
            var blogPost = new BlogPost
            {
                Heading = AddBlogPostRequest.Heading,
                PageTitle = AddBlogPostRequest.PageTitle,
                Author = AddBlogPostRequest.Author,
                Content = AddBlogPostRequest.Content,
                ShortDescription = AddBlogPostRequest.ShortDescription,
                FeaturedImageUrl = AddBlogPostRequest.FeaturedImageUrl,
                UrlHandle = AddBlogPostRequest.UrlHandle,
                PublishedDate = AddBlogPostRequest.PublishedDate,
                Visible = AddBlogPostRequest.Visible,
                Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag()
                {
                    Name = x.Trim()
                }))
            };

            await _blogPostRepository.AddAsync(blogPost);

            var notification = new Notification
            {
                Message = "New blog created!",
                Type = NotificationType.Success
            };
            TempData["Notification"] = JsonSerializer.Serialize(notification);
            return RedirectToPage("/Admin/Blogs/List");
        }

        return Page();
    }

    private void ValidateAddBlogPost()
    {
        
        
        if (AddBlogPostRequest.PublishedDate.Date < DateTime.Now.Date)
        {
            ModelState.AddModelError("AddBlogPostRequest.PublishedDate",
                $"{nameof(AddBlogPostRequest.PublishedDate)} can only be today's date or future date.");
        }
    }
}