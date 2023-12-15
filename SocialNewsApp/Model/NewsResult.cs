using SocialNewsApp.Model.Interfaces;

namespace SocialNewsApp.Model
{
    /// <summary>
    /// Класс новости
    /// </summary>
    public class NewsResult
    {
        public string Title { get; set; }
        public string URI { get; set; }
        public string KeyWord { get; set; }
        public INewsAggregator NewsAggregator { get; set; }
    }
}