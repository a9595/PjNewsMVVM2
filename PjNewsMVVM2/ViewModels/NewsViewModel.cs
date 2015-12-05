﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

        public NewsViewModel()
        {
            //SetDownloadedData();
        }

        public async Task SetDownloadedData()
        {

            _news = new News();
            await _news.DownloadNews();

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
