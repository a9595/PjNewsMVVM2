using System.Collections.Generic;
using System.Threading.Tasks;
using PjNewsMVVM2.Data;
using PjNewsMVVM2.Helpers;
using PJAnews.Model;

namespace PjNewsMVVM2.Model
{
    class News
    {
        // --List Poeple
        public List<Article> Articles { get; set; }


        public News()
        {
            Articles = new List<Article>();
            // TODO: Articels = Service.GetArticles


            //DownloadNews();
        }

        public async Task DownloadNews()
        {
            DownloadedNews downloadedDownloadedNews = await NewsGrabber.GetNews();
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

        private void Add(Article article)
        {
            Articles.Add(article);
        }


    }
}
