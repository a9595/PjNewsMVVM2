using System.Collections.Generic;
using PjNewsMVVM2.Data;
using PjNewsMVVM2.Helpers;
using PJAnews.Model;

namespace PjNewsMVVM2.Model
{
    class NewsNEW
    {
        // --List Poeple
        public List<Article> Articles { get; set; }


        public NewsNEW()
        {
            Articles = new List<Article>();
            // TODO: Articels = Service.GetArticles


            //DownloadNews();
        }

        public void DownloadNews()
        {
            DownloadedNews downloadedDownloadedNews = NewsGrabber.GetNews();
            foreach (var articleDownloaded in downloadedDownloadedNews.Results)
            {
                Article article = new Article(
                    articleDownloaded.Date,
                    articleDownloaded.Link,
                    articleDownloaded.LinkText
                    );
                Add(article);
            }
        }

        public void Add(Article article)
        {
            Articles.Add(article);
        }


    }
}
