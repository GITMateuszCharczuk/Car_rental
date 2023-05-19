using System.ComponentModel.DataAnnotations;
using CarRental.Web.Enums;
using CarRental.Web.Models.Domain;
using CarRental.Web.Models.ViewModels;
using CarRental.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages.Admin.Blogs;

[Authorize(Roles = "Admin")]
public class Edit : PageModel
{
    private readonly IBlogPostRepository _blogPostRepository;

    public Edit(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    [BindProperty] [Required] public string Tags { get; set; }
    [BindProperty] public EditBlogPostRequest BlogPost { get; set; }

    [BindProperty] public IFormFile FeaturedImage { get; set; }

    public async Task OnGet(Guid id)
    {
        var blogPostDomainModel = await _blogPostRepository.GetAsync(id);

        if (blogPostDomainModel != null && blogPostDomainModel.Tags != null)
        {
            BlogPost = new EditBlogPostRequest
            {
                Id = blogPostDomainModel.Id,
                Heading = blogPostDomainModel.Heading,
                Author = blogPostDomainModel.Author,
                Content = blogPostDomainModel.Content,
                FeaturedImageUrl = blogPostDomainModel.FeaturedImageUrl,
                PageTitle = blogPostDomainModel.PageTitle,
                PublishedDate = blogPostDomainModel.PublishedDate,
                ShortDescription = blogPostDomainModel.ShortDescription,
                UrlHandle = blogPostDomainModel.UrlHandle,
                Visible = blogPostDomainModel.Visible
            };

            Tags = string.Join(',', blogPostDomainModel.Tags.Select(x => x.Name));
        }
    }

    public async Task<IActionResult> OnPost()
    {
        ValidateEditBlogPost();
        ModelState["FeaturedImage"]!.ValidationState = ModelValidationState.Valid;
        if (ModelState.IsValid)
            try
            {
                var blogPostDomainModel = new BlogPost
                {
                    Id = BlogPost.Id,
                    Heading = BlogPost.Heading,
                    Author = BlogPost.Author,
                    Content = BlogPost.Content,
                    FeaturedImageUrl = BlogPost.FeaturedImageUrl,
                    PageTitle = BlogPost.PageTitle,
                    PublishedDate = BlogPost.PublishedDate,
                    ShortDescription = BlogPost.ShortDescription,
                    UrlHandle = BlogPost.UrlHandle,
                    Visible = BlogPost.Visible,
                    Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag { Name = x.Trim() }))
                };

                await _blogPostRepository.UpdateAsync(blogPostDomainModel);

                ViewData["Notification"] = new Notification
                {
                    Message = "Record updated succesfully",
                    Type = NotificationType.Success
                };
            }
            catch (Exception ex)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Something went wrong",
                    Type = NotificationType.Error
                };
                throw;
            }


        return Page();
    }

    private void ValidateEditBlogPost()
    {
        if (!string.IsNullOrWhiteSpace(BlogPost.Heading))
            if (BlogPost.Heading.Length < 10 || BlogPost.Heading.Length > 72)
                ModelState.AddModelError("BlogPost.Heading",
                    "Heading between 10 and 72 characters.");
    }
}