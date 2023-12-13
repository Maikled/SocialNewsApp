using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SocialNewsApp.Properties;
using SocialNewsApp.ViewModel;


namespace SocialNewsApp.View
{
    public sealed partial class SettingsPage : Page
    {
        public GeneralViewModel ViewModel { get; set; }

        public int AggregatorNewsCount
        {
            get
            {
                return AppSettings.Default.CountNewsPerAggragator;
            }
            set 
            { 
                AppSettings.Default.CountNewsPerAggragator = value; 
                AppSettings.Default.Save(); 
            }
        }

        public SettingsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is GeneralViewModel viewModel)
                ViewModel = viewModel;

            base.OnNavigatedTo(e);
        }

        private void ButtonBack_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if(MainWindow.MainFrame.CanGoBack)
                MainWindow.MainFrame.GoBack();
        }
    }
}