using CarRental.Web.Models.Domain;
using CarRental.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages.Tags;

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