using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
        public Visibility VisibilityLoading {
            get { return _visibilityLoading; }
            set
            {
                _setProperty = SetProperty(ref _visibilityLoading, value);
            }
        }

        public NewsViewModel()
        {
            //SetDownloadedData();
            VisibilityLoading = _visibilityLoading;
            _news = new News();
        }

        public async Task DownloadNews()
        {
            //Task downloadTask = MainNewsViewModel.DownloadNews();
            //await downloadTask.ContinueWith(OnDownloadCompleted);
            try
            {
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
            }
            catch (Exception)
            {
                
                throw;
            }
            

            VisibilityLoading = Visibility.Collapsed;
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
