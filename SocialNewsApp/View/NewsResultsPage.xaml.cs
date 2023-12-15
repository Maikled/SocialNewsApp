using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SocialNewsApp.Model;
using SocialNewsApp.ViewModel;

namespace SocialNewsApp.View
{
    public sealed partial class NewsResultsPage : Page
    {
        public GeneralViewModel ViewModel { get; set; }

        public NewsResultsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is GeneralViewModel viewModel)
            {
                ViewModel = viewModel;
                ViewModel.KeyWords.CollectionChanged += KeyWords_CollectionChanged;
                ViewModel.SelectedKeyWords.CollectionChanged += KeyWords_CollectionChanged;
                KeyWords_CollectionChanged(null, null);
            }

            base.OnNavigatedTo(e);
        }

        private void KeyWords_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ViewModel.KeyWords.Count > 0)
            {
                if (ViewModel.SelectedKeyWords.Count > 0)
                {
                    messageStackPanel.Visibility = Visibility.Collapsed;
                    newsResults.Visibility = Visibility.Visible;
                }
                else
                {
                    messageStackPanel.Visibility = Visibility.Visible;
                    newsResults.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия на новостной элемент
        /// </summary>
        private void SettingsCard_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var card = sender as FrameworkElement;
            var news = card.DataContext as NewsResult;
            if (news != null)
            {
                var newsPage = new NewsPage() { NewsResult = news, ViewModel = ViewModel };
                MainWindow.MainFrame.Navigate(typeof(NewsPage), newsPage);
            }
        }
    }
}