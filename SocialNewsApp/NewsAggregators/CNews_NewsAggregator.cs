using HtmlAgilityPack;
using SocialNewsApp.Model;
using SocialNewsApp.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SocialNewsApp.NewsAggregators
{
    public class CNews_NewsAggregator : INewsAggregator
    {
        public string Name => "CNews";

        private const string baseURI = "https://www.cnews.ru/search?search=";

        public async Task<IEnumerable<NewsResult>> GetNewsAsync(string query)
        {
            var results = new List<NewsResult>();
            var httpClient = new HttpClient();

            using (var request = new HttpRequestMessage(HttpMethod.Get, baseURI + query))
            {
                using (var responce = await httpClient.SendAsync(request))
                {
                    var stringContent = await responce.Content.ReadAsStringAsync();
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(stringContent);
                    var items = htmlDoc.DocumentNode.Descendants("a").Where(p => p.GetAttributeValue("class", "").Contains("ani-postname"));
                    foreach (var item in items)
                    {
                        results.Add(new NewsResult()
                        {
                            Title = item.InnerHtml,
                            URI = "https:" + item.Attributes["href"].Value,
                            KeyWord = query,
                            NewsAggregator = this
                        });
                    }
                }
            }

            return results;
        }
    }
}
