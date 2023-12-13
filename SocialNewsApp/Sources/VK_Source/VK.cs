using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SocialNewsApp.Model;
using SocialNewsApp.Model.Interfaces;
using SocialNewsApp.Properties;

namespace SocialNewsApp.Sources.VK_Source
{
    public class VK : ISource
    {
        public string ApiVersion { get; } = "5.199";
        public string AuthorizationAddress { get; }

        private string _appID = "51794538";
        private HttpClient _httpClient = new HttpClient();

        public VK()
        {
            AuthorizationAddress = $"https://oauth.vk.com/authorize?client_id={_appID}&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends,wall&response_type=token&v={ApiVersion}&lang=ru";
        }

        public async Task<string> GetSourceTextAsync()
        {
            var posts = await GetPostsAsync(AppSettings.Default.UserToken);

            var stringBuilder = new StringBuilder();
            foreach (var post in posts)
            {
                stringBuilder.Append(post.text);
            }

            return stringBuilder.ToString();
        }

        private async Task<PostItem[]> GetPostsAsync(string token)
        {
            var urlPosts = new Uri($"https://api.vk.com/method/newsfeed.get?access_token={token}&filters=post&v={ApiVersion}&count=100");
            using (var request = new HttpRequestMessage(HttpMethod.Post, urlPosts))
            {
                using (var responce = await _httpClient.SendAsync(request))
                {
                    var content = await responce.Content?.ReadFromJsonAsync<NewsfeedResponce>();
                    return content.response?.items;
                }
            }
        }

        public async Task<bool> CheckAuthorizationToken(string token)
        {
            return await GetPostsAsync(token) != null ? true : false;
        }

        public async Task<AccountPerson> GetAccountPersonAsync(string token)
        {
            var urlAcountInfo = new Uri($"https://api.vk.com/method/account.getProfileInfo?access_token={token}&v={ApiVersion}");
            using(var request =  new HttpRequestMessage(HttpMethod.Post, urlAcountInfo))
            {
                using(var responce = await _httpClient.SendAsync(request))
                {
                    var content = await responce.Content?.ReadFromJsonAsync<AccountInfoResponce>();
                    if(content != null)
                    {
                        return new AccountPerson()
                        {
                            FirstName = content.response.first_name,
                            LastName = content.response.last_name,
                            PhotoPath = content.response.photo_200
                        };
                    }
                    else
                        return null;
                }
            }
        }
    }
}