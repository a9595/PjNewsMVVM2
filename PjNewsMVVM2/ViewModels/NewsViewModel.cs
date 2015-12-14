using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using PjNewsMVVM2.Model;
using Prism.Mvvm;

namespace PjNewsMVVM2.ViewModels
{
    public class NewsViewModel : BindableBase
    {
        //fields
        News _news;
        ObservableCollection<ArticleViewModel> _articles
                    = new ObservableCollection<ArticleViewModel>();
        private int _selectedIndex;
        private bool _setProperty;

        private Visibility _visibilityLoading;
        public Visibility VisibilityLoading
        {
            get { return _visibilityLoading; }
            set
            {
                _setProperty = SetProperty(ref _visibilityLoading, value);
            }
        }

        private Visibility _visibilityProgressRing;
        public Visibility VisibilityProgressRing
        {
            get { return _visibilityProgressRing; }
            set
            {
                _setProperty = SetProperty(ref _visibilityProgressRing, value);
            }
        }

        private string _loadingTextBlockText;

        public string LoadingTextBlockText
        {
            get { return _loadingTextBlockText; }
            set { _setProperty = SetProperty(ref _loadingTextBlockText, value); }
        }


        public NewsViewModel()
        {
            //SetDownloadedData();
            VisibilityLoading = Visibility.Collapsed;
            VisibilityProgressRing = Visibility.Visible; // to show that we're loading the data
            _news = new News();
        }

        public async Task DownloadNews()
        {
            //Task downloadTask = MainNewsViewModel.DownloadNews();
            //await downloadTask.ContinueWith(OnDownloadCompleted);
            if (!IsInternet())
            {
                VisibilityLoading = Visibility.Visible;
                VisibilityProgressRing = Visibility.Collapsed;
                LoadingTextBlockText = "No internet connection :(";
                return;
            }

            Task downloadNewsTask = _news.DownloadNews();
            await downloadNewsTask.ContinueWith(OnDownloadCompleted);

            //add to articles
            foreach (var article in _news.Articles)
            {
                ArticleViewModel newArticleViewModel = new ArticleViewModel(
                    article.Date,
                    article.Link,
                    article.Title
                    );

                _articles.Add(newArticleViewModel);
            }
            VisibilityProgressRing = Visibility.Collapsed;

        }
        public static bool IsInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }
        private void OnDownloadCompleted(Task obj)
        {
            //VisibilityLoading = Visibility.Collapsed;
        }


        public void LoadCachedData()
        {
            _news.LoadCachedNews();
        }



        //properties
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            { SetProperty(ref _selectedIndex, value); }
        }

        public ObservableCollection<ArticleViewModel> Articles
        {
            get { return _articles; }
            set
            { SetProperty(ref _articles, value); }
        }

    }
}
