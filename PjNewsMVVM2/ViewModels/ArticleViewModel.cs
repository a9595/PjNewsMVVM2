using Prism.Mvvm;

namespace PjNewsMVVM2.ViewModels
{
    public class ArticleViewModel : BindableBase
    {
        private string _date;
        private string _link;
        private string _title;
        private string _content;

        public string Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }
        public string Link
        {
            get { return _link; }
            set { SetProperty(ref _link, value); }
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        public ArticleViewModel()
        {


        }

        public ArticleViewModel(string date, string link, string title)
        {
            Date = date;
            Link = link;
            Title = title;
            Content = string.Empty;
        }

        public override string ToString()
        {
            return Date + "  " + Title;
        }
    }
}
