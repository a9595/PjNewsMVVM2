using System;

namespace PJAnews.Model
{
    public class Article
    {
        public string Date { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }


        public Article(string date, string link, string title, string content)
        {
            Date = date;
            Link = link;
            Title = title;
            Content = content;
        }

        public Article(string date, string link, string title)
        {
            Date = date;
            Link = link;
            Title = title;
            Content = String.Empty;
        }

        public Article()
        {

        }
    }
}