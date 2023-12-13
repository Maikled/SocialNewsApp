using HtmlAgilityPack;
using SocialNewsApp.Model;
using SocialNewsApp.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SocialNewsApp.NewsAggregators
{
    public class Comnews24_NewsAggregator : INewsAggregator
    {
        public string Name => "Comnews24";
        
        private const string baseURI = "https://comnews24.ru/?s=";

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
                    var items = htmlDoc.DocumentNode.Descendants("h2").Where(p => p.GetAttributeValue("class", "").Contains("entry-title"));
                    foreach (var item in items)
                    {
                        var a = item.FirstChild;
                        if(a != null)
                        {
                            results.Add(new NewsResult()
                            {
                                Title = a.InnerHtml,
                                URI = a.Attributes["href"].Value,
                                KeyWord = query,
                                NewsAggregator = this
                            });
                        }
                    }
                }
            }

            return results;
        }
    }
}
