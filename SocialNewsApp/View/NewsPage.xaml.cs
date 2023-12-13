using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SocialNewsApp.Model;
using SocialNewsApp.ViewModel;

namespace SocialNewsApp.View
{
    public sealed partial class NewsPage : Page
    {
        public NewsResult NewsResult { get; set; }
        public GeneralViewModel ViewModel { get; set; }

        public NewsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                var newsPage = e.Parameter as NewsPage;
                this.NewsResult = newsPage.NewsResult;
                this.ViewModel = newsPage.ViewModel;
            }
        }

        private void AppBarButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if(MainWindow.MainFrame.CanGoBack)
                MainWindow.MainFrame.GoBack();
        }
    }
}