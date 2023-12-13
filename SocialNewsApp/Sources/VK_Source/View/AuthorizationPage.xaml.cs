using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SocialNewsApp.Sources.VK_Source.ViewModel;
using SocialNewsApp.View;

namespace SocialNewsApp.Sources.VK_Source.View
{
    public sealed partial class AuthorizationPage : Page
    {
        public VKAuthorizationViewModel ViewModel { get; set; }

        public AuthorizationPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is VKAuthorizationViewModel viewModel)
                ViewModel = viewModel;

            base.OnNavigatedTo(e);
        }

        private void webView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel?.ConfigureWebView(webView);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Navigate(typeof(StartPage));
        }
    }
}