using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PjNewsMVVM2.Model;

namespace PjNewsMVVM2.Data
{
    class NewsNEW
    {
        // --List Poeple
        public List<ArticleNEW> Articles { get; set; }


        public NewsNEW(News news)
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
