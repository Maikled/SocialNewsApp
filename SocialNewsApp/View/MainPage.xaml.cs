using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SocialNewsApp.ViewModel;

namespace SocialNewsApp.View
{
    public sealed partial class MainPage : Page
    {
        public GeneralViewModel ViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is GeneralViewModel viewModel)
            {
                ViewModel = viewModel;
            }

            base.OnNavigatedTo(e);

            LoadKeyWordsFrame();
            LoadNewsResultsFrame();
            LoadAccountPerson();
        }

        private void LoadKeyWordsFrame()
        {
            keyWordsFrame.Navigate(typeof(KeyWordsPage), ViewModel);
        }

        private void LoadNewsResultsFrame()
        {
            newsResultsFrame.Navigate(typeof(NewsResultsPage), ViewModel);
        }

        private void LoadAccountPerson()
        {
            var accountButton = accountButtonDataTemplate.LoadContent() as Button;
            accountButton.DataContext = ViewModel.AccountPerson;

            MainWindow.LoadPersonPicture(accountButton);
        }

        private void MenuFlyoutItemLeave_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearAccountData();

            MainWindow.ContentControl.Content = null;
            MainWindow.MainFrame.Navigate(typeof(StartPage));
        }

        private void MenuFlyoutItemSettings_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Navigate(typeof(SettingsPage), ViewModel);
        }
    }
}