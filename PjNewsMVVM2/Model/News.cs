using System.Collections.Generic;
using System.Threading.Tasks;
using PjNewsMVVM2.Data;
using PjNewsMVVM2.Helpers;
using PJAnews.Model;
using PJAnews.Helpers;

namespace PjNewsMVVM2.Model
{
    class News
    {
        // --List Poeple
        public List<Article> Articles { get; set; }
        DataSaver<List<Article>> ArticlesSerializer = new DataSaver<List<Article>>();
        private string _savedArticleFileName = "savedArticles";


        public News()
        {
            Articles = new List<Article>();
            // TODO: Articels = Service.GetArticles


            //DownloadNews();
        }

        public void LoadCachedNews()
        {
            //1. try to load:
            List<Article> articlesLoaded = new List<Article>();
            try
            {
                articlesLoaded = ArticlesSerializer.LoadMyData(_savedArticleFileName);
                if (articlesLoaded.Count > 0)
                    Articles = articlesLoaded;
            }
            catch (System.Exception ex)
            {

            }
        }

        public async Task DownloadNews()
        {
            //2. download
            DownloadedNews downloadedDownloadedNews = await NewsGrabber.GetNews();

            //3. if there fresh news - p
            if (downloadedDownloadedNews.Results.Count > Articles.Count)
            {
                foreach (var articleDownloaded in downloadedDownloadedNews.Results)
                {
                    Article article = new Article(
                        articleDownloaded.Date,
                        articleDownloaded.Link,
                        articleDownloaded.LinkText
                        );
                    Add(article);
                }
                ArticlesSerializer.SaveMyData(Articles, _savedArticleFileName);
            }


        }



        private void Add(Article article)
        {
            Articles.Add(article);
        }


    }
}
