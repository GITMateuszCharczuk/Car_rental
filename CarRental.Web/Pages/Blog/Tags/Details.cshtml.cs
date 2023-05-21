using CarRental.Web.Models.Domain;
using CarRental.Web.Models.Domain.Blog;
using CarRental.Web.Repositories;
using CarRental.Web.Repositories.BlogRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages.Blog.Tags;

public class Details : PageModel
{
    private readonly IBlogPostRepository _blogPostRepository;

    public Details(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    public List<BlogPost> Blogs { get; set; }

    public async Task<IActionResult> OnGet(string tagName)
    {
        Blogs = (await _blogPostRepository.GetAllAsync(tagName)).ToList();
        return Page();
    }
}