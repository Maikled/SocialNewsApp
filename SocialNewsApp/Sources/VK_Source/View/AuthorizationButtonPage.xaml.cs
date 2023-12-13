using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SocialNewsApp.Properties;
using SocialNewsApp.ViewModel;
using SocialNewsApp.View;
using SocialNewsApp.Sources.VK_Source.ViewModel;

namespace SocialNewsApp.Sources.VK_Source.View
{
    public sealed partial class AuthorizationButtonPage : Page
    {
        public AuthorizationButtonPage()
        {
            this.InitializeComponent();
        }

        private void sendDataHyperlink_Click(object sender, RoutedEventArgs e)
        {
            sendDataTeachingTip.IsOpen = true;
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = new VKAuthorizationViewModel();
            viewModel.GetTokenCompleted += async (token) =>
            {
                AppSettings.Default.UserToken = token;
                AppSettings.Default.Save();

                var generalViewModel = new GeneralViewModel();
                generalViewModel.AccountPerson = await generalViewModel.LoadAccountPerson();

                MainWindow.MainFrame.Navigate(typeof(MainPage), generalViewModel);
                generalViewModel.LoadKeyWordsAsync();
            };

            MainWindow.MainFrame.Navigate(typeof(AuthorizationPage), viewModel);
        }
    }
}