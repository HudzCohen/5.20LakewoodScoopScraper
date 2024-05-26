using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace LakewoodScoopScraper.Web.Services
{

    public class NewsItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
    }


    public class Scraper
    {
        public List<NewsItem> ScrapeNewsItem()
        {
            var query = "https://thelakewoodscoop.com";
            var html = GetNewsHtml(query);
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);

            var resultDivs = document.QuerySelectorAll("div.td-category-pos-image");
            return resultDivs.Select(div => ParseItem(div)).Where(i => i != null).ToList();

        }

        private static string GetNewsHtml(string query)
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate,
                UseCookies = true
            };

            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US");

            return client.GetStringAsync(query).Result;
        }

        private static NewsItem ParseItem(IElement div)
        {
            var newsItem = new NewsItem();

            var titleElement = div.QuerySelector("h3.td-module-title");
            newsItem.Title = titleElement?.TextContent;
                
            var anchorTag = div.QuerySelector("h3.td-module-title a");
            newsItem.Url = anchorTag?.Attributes["href"].Value;

            var imageElement = div.QuerySelector("span.entry-thumb");
            newsItem.Image = imageElement?.Attributes["data-img-url"].Value;
            
            return newsItem;
        }
    }
}
