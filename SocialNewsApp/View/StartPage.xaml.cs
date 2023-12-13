using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SocialNewsApp.Properties;
using SocialNewsApp.Sources.VK_Source;
using SocialNewsApp.Sources.VK_Source.View;
using SocialNewsApp.ViewModel;

namespace SocialNewsApp.View
{
    public sealed partial class StartPage : Page
    {
        public StartPage()
        {
            this.InitializeComponent();
        }

        private async void authorizationFrame_Loaded(object sender, RoutedEventArgs e)
        {
            var vk = new VK();
            var result = await vk.CheckAuthorizationToken(AppSettings.Default.UserToken);
            if(result == false)
            {
                authorizationFrame.Navigate(typeof(AuthorizationButtonPage));
            }
            else
            {
                var viewModel = new GeneralViewModel();
                viewModel.AccountPerson = await viewModel.LoadAccountPerson();

                MainWindow.MainFrame.Navigate(typeof(MainPage), viewModel);
                viewModel.LoadKeyWordsAsync();
            }
        }
    }
}