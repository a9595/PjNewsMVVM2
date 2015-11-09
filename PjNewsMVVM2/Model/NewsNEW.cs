using System.Collections.Generic;
using PjNewsMVVM2.Data;
using PjNewsMVVM2.Helpers;

namespace PjNewsMVVM2.Model
{
    class NewsNEW
    {
        // --List Poeple
        public List<ArticleNEW> Articles { get; set; }


        public NewsNEW()
        {
            Articles = new List<ArticleNEW>();
            // TODO: Articels = Service.GetArticles
            News downloadedNews = NewsGrabber.GetNews();
            foreach (var articleDownloaded in downloadedNews.Results)
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
