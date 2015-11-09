using System.Collections.ObjectModel;
using PjNewsMVVM2.Model;
using Prism.Mvvm;

namespace PjNewsMVVM2.ViewModels
{
    public class NewsViewModel : BindableBase
    {
        //fields
        NewsNEW news;
        ObservableCollection<ArticleViewModel> _articles
                    = new ObservableCollection<ArticleViewModel>();
        private int _selectedIndex;

        public NewsViewModel()
        {
            news = new NewsNEW();
            foreach (var article in news.Articles)
            {
                ArticleViewModel newArticleViewModel = new ArticleViewModel(
                    article.Date,
                    article.Link,
                    article.Title
                    );

                _articles.Add(newArticleViewModel);
            }
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
