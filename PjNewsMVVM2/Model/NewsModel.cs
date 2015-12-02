using System.Collections.Generic;
using System.Threading.Tasks;
using PjNewsMVVM2.Data;
using PjNewsMVVM2.Helpers;

namespace PjNewsMVVM2.Model
{
    class NewsModel
    {
        // --List Poeple
        public List<ArticleNEW> Articles { get; set; }


        public NewsModel()
        {
            Articles = new List<ArticleNEW>();
            // TODO: Articels = Service.GetArticles
            DownloadNews();
        }

        private  void DownloadNews()
        {
            DownloadedNews downloadedDownloadedNews =  NewsGrabber.GetNews();
            foreach (var articleDownloaded in downloadedDownloadedNews.Results)
            {
                ArticleNEW newArticle = new ArticleNEW(
                    articleDownloaded.Date,
                    articleDownloaded.Link,
                    articleDownloaded.LinkText
                    );
                Add(newArticle);
            }
        }

        public void Add(ArticleNEW articleNew)
        {
            Articles.Add(articleNew);
        }


    }
}
