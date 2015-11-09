using System;

namespace PjNewsMVVM2.Data
{
    public class ArticleNEW
    {
        public string Date { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }


        public ArticleNEW(string date, string link, string title, string content)
        {
            Date = date;
            Link = link;
            Title = title;
            Content = content;
        }

        public ArticleNEW(string date, string link, string title)
        {
            Date = date;
            Link = link;
            Title = title;
            Content = String.Empty;
        }

        public ArticleNEW()
        {
            
        }
    }
}