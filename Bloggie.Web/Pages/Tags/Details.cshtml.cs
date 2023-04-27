using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Tags;

public class Details : PageModel
{
    private readonly IBlogPostRepository _blogPostRepository;

    public List<BlogPost> Blogs { get; set; }

    public Details(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    public async Task<IActionResult> OnGet(string tagName)
    {
        Blogs = (await _blogPostRepository.GetAllAsync(tagName)).ToList();
        return Page();
    }
}