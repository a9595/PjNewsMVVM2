using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;
using PjNewsMVVM2.Helpers;

namespace PjNewsMVVM2.Data
{
    public class NewsGrabber
    {

        private static string _urlNews =
            "https://api.import.io/store/data/ac6a058d-2785-42b6-b1d9-b20c4d48c8f7/_query?input/webpage/url=http%3A%2F%2Fwww.pja.edu.pl%2Faktualnosci%2Fglowna&_user=ac4a2596-0302-46ee-a01a-153a5b50f8bf&_apikey=ac4a2596030246eea01a153a5b50f8bf8d83fcfebeb20555e1c978bf8baa34cc8783b48aa9648c98236227aa39e38c716a3280346535778f39005f54d0a00eb45cdf4387ab49e5af783d95afa60b5c37";

        private string filename = "newsJsons";

        public static DownloadedArticle GetAlternativeArticleSimply(string url)
        {
            string urlPartAPI1 =
                "https://api.import.io/store/data/40ab96a9-c714-4844-9eb6-20bd86cf8501/_query?input/webpage/url=";
            string urlPartApiKey3 = "&_user=ac4a2596-0302-46ee-a01a-153a5b50f8bf&_apikey=ac4a2596030246eea01a153a5b50f8bf8d83fcfebeb20555e1c978bf8baa34cc8783b48aa9648c98236227aa39e38c716a3280346535778f39005f54d0a00eb45cdf4387ab49e5af783d95afa60b5c37";



            string resultUrl = urlPartAPI1 + url + urlPartApiKey3;
            Uri requestUri = new Uri(resultUrl, UriKind.Absolute);



            var httpClient = new HttpClient();
            var payload = httpClient.GetStringAsync(requestUri).Result;

            var sampleResponse = JsonConvert.DeserializeObject<DownloadedArticle>(payload,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });


            //var sampleResponse = JsonConvert.DeserializeObject<DownloadedArticle>(payload);


            return sampleResponse;
        }


        public static DownloadedNews GetNews()
        {
            var httpClient = new HttpClient();
            var payload = httpClient.GetStringAsync(_urlNews).Result;

            var news = JsonConvert.DeserializeObject<DownloadedNews>(payload,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

            ArrayList toChangeNews = new ArrayList((ICollection) news.Results);
            
            //
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
            var newsEditedDates = news.Results.Skip(Math.Max(0, news.Results.Count() - 10));
            
            //
            

            return news;
        }



        // Write the Json string in the JSONFILENAME.
        private async Task writeJsonAsync(DownloadedNews newsJson)
        {
            var serializer = new DataContractJsonSerializer(typeof(DownloadedNews));

            filename = "filename";
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                                filename,
                                CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, newsJson);
            }
        }

        // Read the Json string stored in the JSONFILENAME.
        private async Task readJsonAsync()
        {
            string content = String.Empty;
            DownloadedNews ListGameScore = new DownloadedNews();

            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(filename);

            using (StreamReader reader = new StreamReader(myStream))
            {
                content = await reader.ReadToEndAsync();
            }

        }



    }
}
