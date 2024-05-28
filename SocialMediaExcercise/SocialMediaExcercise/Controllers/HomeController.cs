using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SocialMediaExcercise.Controllers
{
    public class HomeController : Controller
    {
        private readonly SocialMediaLinksOptions _options;

        public HomeController(IOptions<SocialMediaLinksOptions> options)
        {
            _options = options.Value;
        }

        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.Socials = _options;
            return View();
        }
    }
}
