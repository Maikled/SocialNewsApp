using Microsoft.UI.Xaml.Controls;
using System;

namespace SocialNewsApp.Sources.VK_Source.ViewModel
{
    public class VKAuthorizationViewModel
    {
        public delegate void GetToken(string token);
        public event GetToken GetTokenCompleted;

        public async void ConfigureWebView(WebView2 webView)
        {
            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.CookieManager.DeleteAllCookies();
            webView.CoreWebView2.AddWebResourceRequestedFilter("https://oauth.vk.com/blank.html*", Microsoft.Web.WebView2.Core.CoreWebView2WebResourceContext.All);
            webView.CoreWebView2.WebResourceResponseReceived += CoreWebView2_WebResourceResponseReceived;

            var vk = new VK();
            webView.Source = new Uri(vk.AuthorizationAddress);
        }

        private void CoreWebView2_WebResourceResponseReceived(Microsoft.Web.WebView2.Core.CoreWebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2WebResourceResponseReceivedEventArgs args)
        {
            try
            {
                if (args.Request.Method == "GET")
                {
                    var locationHeader = args.Response.Headers.GetHeader("location");
                    var uri = new Uri(locationHeader);
                    if (uri != null)
                    {
                        var trimFragment = uri.Fragment?.TrimStart('#');
                        var arguments = trimFragment.Split('&');
                        foreach (var argument in arguments)
                        {
                            if (argument.Contains("access_token"))
                            {
                                var token = argument.Substring(argument.IndexOf("=") + 1);
                                if (!string.IsNullOrEmpty(token))
                                {
                                    GetTokenCompleted?.Invoke(token);

                                    sender.WebResourceResponseReceived -= CoreWebView2_WebResourceResponseReceived;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }
}