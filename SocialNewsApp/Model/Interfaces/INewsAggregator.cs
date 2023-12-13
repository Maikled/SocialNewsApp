using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNewsApp.Model.Interfaces
{
    public interface INewsAggregator
    {
        public string Name { get; }
        public Task<IEnumerable<NewsResult>> GetNewsAsync(string query);
    }
}