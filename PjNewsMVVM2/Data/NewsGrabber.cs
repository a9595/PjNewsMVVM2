using System;
using System.IO;

using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Web.Http;
using Newtonsoft.Json;
using PjNewsMVVM2.Helpers;

namespace PjNewsMVVM2.Data
{
    public class NewsGrabber
    {

        private static string _urlNews =
            "https://api.import.io/store/data/ac6a058d-2785-42b6-b1d9-b20c4d48c8f7/_query?input/webpage/url=http%3A%2F%2Fwww.pja.edu.pl%2Faktualnosci%2Fglowna&_user=ac4a2596-0302-46ee-a01a-153a5b50f8bf&_apikey=ac4a2596030246eea01a153a5b50f8bf8d83fcfebeb20555e1c978bf8baa34cc8783b48aa9648c98236227aa39e38c716a3280346535778f39005f54d0a00eb45cdf4387ab49e5af783d95afa60b5c37";

        private string filename = "newsJsons";

        public static async Task<DownloadedArticle> GetAlternativeArticleSimply(string url)
        {
            string urlPartAPI1 =
                "https://api.import.io/store/data/40ab96a9-c714-4844-9eb6-20bd86cf8501/_query?input/webpage/url=";
            string urlPartApiKey3 = "&_user=ac4a2596-0302-46ee-a01a-153a5b50f8bf&_apikey=ac4a2596030246eea01a153a5b50f8bf8d83fcfebeb20555e1c978bf8baa34cc8783b48aa9648c98236227aa39e38c716a3280346535778f39005f54d0a00eb45cdf4387ab49e5af783d95afa60b5c37";



            string resultUrl = urlPartAPI1 + url + urlPartApiKey3;
            Uri requestUri = new Uri(resultUrl, UriKind.Absolute);



            var httpClient = new HttpClient();
            var payload = await httpClient.GetStringAsync(requestUri);


            var sampleResponse = JsonConvert.DeserializeObject<DownloadedArticle>(payload,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });


            //var sampleResponse = JsonConvert.DeserializeObject<DownloadedArticle>(payload);


            return sampleResponse;
        }


        public static async Task<DownloadedNews> GetNews()
        {
            var httpClient = new HttpClient();
            //var payload = await httpClient.GetStringAsync(_urlNews);
            var payload = await httpClient.GetStringAsync(new Uri(_urlNews, UriKind.Absolute));

            var news = JsonConvert.DeserializeObject<DownloadedNews>(payload,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });



            SetFixedDateYear(news);


            return news;
        }

        private static void SetFixedDateYear(DownloadedNews news)
        {
            for (int i = news.Results.Count; i-- > news.Results.Count - 15;)
            {
                //do something
                news.Results[i].Date += ".14";
            }

            int deltaCount = 0;
            int intarator = 0;
            do
            {
                news.Results[intarator].Date += ".15";

                intarator++;
                deltaCount = news.Results.Count - intarator;
            } while (deltaCount > 15);
        }




    }
}
