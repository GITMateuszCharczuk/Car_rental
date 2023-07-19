using CarRental.Web.Models.Domain;
using CarRental.Web.Models.Domain.Blog;
using CarRental.Web.Models.Domain.CarOffer;
using CarRental.Web.Repositories;
using CarRental.Web.Repositories.BlogRepo;
using CarRental.Web.Repositories.CarBDRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Pages;

public class IndexModel : PageModel
{

    private readonly ILogger<IndexModel> _logger;
    private readonly ICarOfferRepository _carOfferRepository;
    private readonly IBlogPostRepository _blogPostRepository;

    public List<CarOffer> CarOffers { get; set; }

    public List<BlogPost> BlogPosts { get; set; }
    
    public List<BlogPost> LatestBlogPosts { get; set; }

    public IndexModel(ILogger<IndexModel> logger, ICarOfferRepository carOfferRepository, IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
        _carOfferRepository = carOfferRepository;
        _logger = logger;
    }
    

    public async Task<IActionResult> OnGet()
    {
        try
        {
            CarOffers = (await _carOfferRepository.GetAllAsync()).ToList();
            BlogPosts = (await _blogPostRepository.GetAllAsync()).ToList();
        }
        catch (Exception e) { ; }
        if (BlogPosts.Count >= 3)
        {
            BlogPosts.Sort((x, y) => x.PublishedDate.CompareTo(y.PublishedDate));
            LatestBlogPosts = BlogPosts.GetRange(0, 3);
        }
        else
        {
            LatestBlogPosts = null;
        }
        return Page();
    }
}