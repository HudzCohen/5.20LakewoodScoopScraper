using LakewoodScoopScraper.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LakewoodScoopScraper.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsItemScrapeController : ControllerBase
    {
        [Route("scrapenews")]
        public List<NewsItem> ScrapeNews()
        {
            var scraper = new Scraper();
            return scraper.ScrapeNewsItem();
        }
    }
}
