using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TinyURLService.Models;

namespace TinyURLService.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUrlShortenerService urlShortenerService;

        public UrlController(IUrlShortenerService urlShortenerService)
        {
            this.urlShortenerService = urlShortenerService;
        }

        public ActionResult Index()
        {
            // Display UI to interact with the service
            return View();
        }

        [HttpPost]
        public ActionResult ShortenUrl(string longUrl, string customShortUrl)
        {
            string shortUrl = urlShortenerService.ShortenUrl(longUrl, customShortUrl);
            // Return the short URL to the frontend
            return Content(shortUrl);
        }

        [HttpGet]
        public ActionResult GetLongUrl(string shortUrl)
        {
            string longUrl = urlShortenerService.GetLongUrl(shortUrl);
            // Redirect to the long URL
            return Redirect(longUrl);
        }

        [HttpGet]
        public ActionResult GetClickCount(string shortUrl)
        {
            int clickCount = urlShortenerService.GetClickCount(shortUrl);
            // Return the click count to the frontend
            return Content(clickCount.ToString());
        }

        [HttpPost]
        public ActionResult DeleteUrl(string shortUrl)
        {
            urlShortenerService.DeleteUrl(shortUrl);
            // Return success message
            return Content("URL deleted successfully");
        }
    }
}
