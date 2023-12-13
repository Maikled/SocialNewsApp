using System.Threading.Tasks;

namespace SocialNewsApp.Model.Interfaces
{
    public interface ISource
    {
        public Task<string> GetSourceTextAsync();
        public Task<bool> CheckAuthorizationToken(string token);
        public Task<AccountPerson> GetAccountPersonAsync(string token);
    }
}