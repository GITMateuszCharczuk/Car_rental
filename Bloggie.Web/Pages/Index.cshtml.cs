using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ILogger<IndexModel> _logger;
    private readonly ITagRepository _tagRepository;

    public IndexModel(ILogger<IndexModel> logger, IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
        _blogPostRepository = blogPostRepository;
        _logger = logger;
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