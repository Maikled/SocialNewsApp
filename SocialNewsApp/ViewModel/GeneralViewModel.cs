using Microsoft.UI.Dispatching;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SocialNewsApp.Model;
using SocialNewsApp.Model.Interfaces;
using SocialNewsApp.Properties;
using SocialNewsApp.Sources.VK_Source;
using SocialNewsApp.TextAnalyzers;

namespace SocialNewsApp.ViewModel
{
    public class GeneralViewModel
    {
        public ObservableCollection<KeyWord> KeyWords { get; set; } = new ObservableCollection<KeyWord>();
        public ObservableCollection<KeyWord> SelectedKeyWords { get; set; } = new ObservableCollection<KeyWord>();
        public ObservableCollection<NewsResult> NewsResults { get; set; } = new ObservableCollection<NewsResult>();
        public ICollection<INewsAggregator> Aggregators { get; set; }
        public IEnumerable<string> AllKeyWords { get; set; } = new List<string>();
        public int LastCount { get; private set; } = 0;
        public bool IsEndedKeyWords { get; private set; } = false;
        public AccountPerson AccountPerson { get; set; }
        
        public event Action OnKeyWordsLoaded;

        public GeneralViewModel()
        {
            LoadNewsAggregation();
        }

        private void LoadNewsAggregation()
        {
            Aggregators = new List<INewsAggregator>();

            var currentNewsAggregatorsTypes = Assembly.GetExecutingAssembly().GetTypes().Where(p => p.GetInterfaces().Contains(typeof(INewsAggregator)));
            foreach (var type in currentNewsAggregatorsTypes)
            {
                var aggregator = Activator.CreateInstance(type) as INewsAggregator;
                Aggregators.Add(aggregator);
            }
        }

        public async Task<AccountPerson> LoadAccountPerson()
        {
            var account = await new VK().GetAccountPersonAsync(AppSettings.Default.UserToken);
            if (AccountPerson != null)
            {
                AppSettings.Default.UserFirstName = account.FirstName;
                AppSettings.Default.UserLastName = account.LastName;
                AppSettings.Default.UserPhotoPath = account.PhotoPath;

                AppSettings.Default.Save();
            }

            return account;
        }

        public async void LoadKeyWordsAsync()
        {
            var text = await new VK().GetSourceTextAsync();
            if (!string.IsNullOrWhiteSpace(text))
            {
                var textAnalyzer = new PullentiTextAnalyzer();
                var stringKeyWords = textAnalyzer.Analyze(text);

                if (stringKeyWords.Count() > 0)
                {
                    AllKeyWords = stringKeyWords;
                    TakeKeyWords(10);

                    OnKeyWordsLoaded?.Invoke();
                }
            }
        }

        public void TakeKeyWords(int count)
        {
            var getElements = AllKeyWords.Skip(LastCount).Take(count);

            var getElementsCount = getElements.Count();
            if (getElementsCount == 0)
            {
                IsEndedKeyWords = true;
                return;
            }

            foreach (var word in getElements)
            {
                KeyWords.Add(new KeyWord(word));
            }

            LastCount += getElementsCount;
        }

        public void AddChoosedKeyWord(KeyWord keyWord)
        {
            keyWord.IsSelected = true;
            if (!SelectedKeyWords.Contains(keyWord))
            {
                SelectedKeyWords.Add(keyWord);
                SearchNewsByKeyWord(keyWord.Word);
            }
        }

        public void RemoveChoosedKeyWord(KeyWord keyWord)
        {
            keyWord.IsSelected = false;
            if (SelectedKeyWords.Contains(keyWord))
            {
                RemoveNewsByKeyWord(keyWord.Word);
                SelectedKeyWords.Remove(keyWord);
            }
        }

        private void SearchNewsByKeyWord(string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
                return;

            foreach (var newsAggreagator in Aggregators)
            {
                Task.Run(async () =>
                {
                    var newsByKeyWord = await newsAggreagator.GetNewsAsync(keyWord);

                    foreach (var item in newsByKeyWord.Take(AppSettings.Default.CountNewsPerAggragator))
                    {
                        MainWindow.UIDispatcher.TryEnqueue(DispatcherQueuePriority.Low, () =>
                        {
                            NewsResults.Insert(0, item);
                        });
                    }

                    await Task.Delay(1000);
                });
            }
        }

        private void RemoveNewsByKeyWord(string keyWord)
        {
            var newsByKeyWord = NewsResults.Where(p => p.KeyWord == keyWord).ToList();
            foreach (var item in newsByKeyWord)
            {
                NewsResults.Remove(item);
            }
        }

        public void ClearAccountData()
        {
            AppSettings.Default.UserToken = null;
            AppSettings.Default.UserFirstName = null;
            AppSettings.Default.UserLastName = null;
            AppSettings.Default.UserPhotoPath = null;

            AppSettings.Default.Save();
        }
    }
}
