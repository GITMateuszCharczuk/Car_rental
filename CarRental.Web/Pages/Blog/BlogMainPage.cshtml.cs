using CarRental.Web.Models.Domain.Blog;
using CarRental.Web.Repositories.BlogRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages.Blog;

public class BlogMainPage : PageModel
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ITagRepository _tagRepository;

    public BlogMainPage( IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
        _blogPostRepository = blogPostRepository;
    }

    [BindProperty] public List<BlogPost> Blogs { get; set; }
    [BindProperty] public List<Tag> Tags { get; set; }

    public async Task<IActionResult> OnGet()
    {
        Blogs = (await _blogPostRepository.GetAllAsync()).ToList();
        Tags = (await _tagRepository.GetAllAsync()).ToList();
        return Page();
    }
}