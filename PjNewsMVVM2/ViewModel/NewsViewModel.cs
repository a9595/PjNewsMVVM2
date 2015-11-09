using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Interop;
using PjNewsMVVM2.Data;
using PjNewsMVVM2.Model;
using Prism.Mvvm;

namespace PjNewsMVVM2.ViewModel
{
    class NewsViewModel : BindableBase
    {
        //fields
        News news;
        ObservableCollection<ArticleViewModel> _articles
                    = new ObservableCollection<ArticleViewModel>();
        private int _selectedIndex;

        public NewsViewModel()
        {
            //var downloadedNews = NewsGrabber.GetNews();

            //foreach (var VARIABLE in downloadedNews)
            //{
                
            //}
            //news = new News();

        }


        //properties
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            { SetProperty(ref _selectedIndex, value); }
        }

        public ObservableCollection<ArticleViewModel> Articles
        { get { return _articles; }
            set {SetProperty(ref _articles, value);} }






    }
}
