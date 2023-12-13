using HtmlAgilityPack;
using SocialNewsApp.Model;
using SocialNewsApp.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SocialNewsApp.NewsAggregators
{
    public class RT_NewsAggregator : INewsAggregator
    {
        public string Name => "RT";

        private const string baseURI = "https://russian.rt.com/search?q=";
        private const string baseLink = "https://russian.rt.com";

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
                    var items = htmlDoc.DocumentNode.Descendants("a").Where(p => p.GetAttributeValue("class", "").Contains("link link_color"));
                    foreach (var item in items)
                    {
                        results.Add(new NewsResult()
                        {
                            Title = item.InnerHtml.Replace("\n", "").Trim(),
                            URI = baseLink + item.Attributes["href"].Value,
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
