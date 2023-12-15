using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Navigation;
using SocialNewsApp.Model;
using SocialNewsApp.ViewModel;
using System.Linq;

namespace SocialNewsApp.View
{
    /// <summary>
    /// Обработчик страницы ключевых слов
    /// </summary>
    public sealed partial class KeyWordsPage : Page
    {
        public GeneralViewModel ViewModel { get; set; }

        public KeyWordsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is GeneralViewModel viewModel)
            {
                ViewModel = viewModel;
                ViewModel.OnKeyWordsLoaded += ShowPageContent;
            }

            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Обработчик кнопки "Загрузить больше ключевых слов"
        /// </summary>
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.TakeKeyWords(5);

            if(ViewModel.KeyWords.Count == ViewModel.AllKeyWords.Count())
            {
                var hyperlinkButton = sender as HyperlinkButton;
                hyperlinkButton.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку ключевого слова
        /// </summary>
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var toggleButton = sender as ToggleButton;
            if(toggleButton != null)
            {
                if (toggleButton.IsChecked == true)
                {
                    ViewModel.AddChoosedKeyWord(toggleButton?.Tag as KeyWord);
                }
                if (toggleButton.IsChecked == false)
                {
                    ViewModel.RemoveChoosedKeyWord(toggleButton?.Tag as KeyWord);
                }
            }
        }

        private void ToggleButton_Loaded(object sender, RoutedEventArgs e)
        {
            var toggleButton = sender as ToggleButton;
            var keyWord = toggleButton?.Tag as KeyWord;
            if(keyWord != null)
            {
                if(keyWord.IsSelected)
                    toggleButton.IsChecked = true;
                else
                    toggleButton.IsChecked = false;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ShowPageContent();
        }

        /// <summary>
        /// Метод определния отображения шаблона контента ключевых слов
        /// </summary>
        private void ShowPageContent()
        {
            if (ViewModel.KeyWords.Count == 0)
            {
                loadingStackPanel.Visibility = Visibility.Visible;
                keyWordsGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                loadingStackPanel.Visibility = Visibility.Collapsed;
                keyWordsGrid.Visibility = Visibility.Visible;
            }
        }
    }
}