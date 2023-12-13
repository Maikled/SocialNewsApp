using HtmlAgilityPack;
using SocialNewsApp.Model;
using SocialNewsApp.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SocialNewsApp.NewsAggregators
{
    public class AIF_NewsAggregator : INewsAggregator
    {
        public string Name => "Аргументы и Факты";

        private const string baseURI = "https://aif.ru/search?text=";

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
                    var items = htmlDoc.DocumentNode.Descendants("div").Where(p => p.GetAttributeValue("class", "").Contains("text_box"));
                    foreach (var item in items)
                    {
                        var a = item.ChildNodes.FirstOrDefault(p => p.Name == "a");
                        if(a != null)
                        {
                            results.Add(new NewsResult()
                            {
                                Title = a.InnerText,
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
