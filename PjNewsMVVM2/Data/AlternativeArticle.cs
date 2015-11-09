using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PjNewsMVVM2.Data
{
    public class Result
    {
        public Result()
        {
            //TODO:add images support
            //Image = new List<string>();
            //ImageSource = new List<string>();
            //ImageAlt = new List<string>(); 
        }

        //TODO:add images support
        //[JsonProperty("image/_alt")]
        //public List<string> ImageAlt { get; set; }

        //[JsonProperty("image")]
        //public List<string> Image { get; set; }

        //[JsonProperty("image/_source")]
        //public List<string> ImageSource { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }


        [JsonProperty("content")]
        public string Content { get; set; }
    }

    public class ArticleAlternative
    {
        public ArticleAlternative()
        {
        }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        //[JsonProperty("results")]
        //public Result Results { get; set; }

        [JsonProperty("results")]
        public IList<Result> Results { get; set; }

        [JsonProperty("cookies")]
        public IList<string> Cookies { get; set; }

        [JsonProperty("connectorVersionGuid")]
        public string ConnectorVersionGuid { get; set; }

        [JsonProperty("connectorGuid")]
        public string ConnectorGuid { get; set; }

        [JsonProperty("pageUrl")]
        public string PageUrl { get; set; }

        [JsonProperty("outputProperties")]
        public IList<OutputPropertyAlternative> OutputProperties { get; set; }



        #region Methods added by me

        public string GetTitle()
        {
            //return Results.Title;

            return Results.First().Title;

        }

        public string GetContent()
        {
            //string delimeter = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
            //results.First().content.Aggregate((i, j) => i + delimeter + j);

            //var allContent = "";
            //foreach (var str in Results.First().Content)
            //{
            //    allContent = allContent + str;
            //}

            var content = Results.First().Content;
            return content ?? "no content";
        }

        //public async Task<BitmapImage> GetHeaderImage()
        //{
        //    var httpClient = new HttpClient();
        //    var contentBytes = await httpClient.GetByteArrayAsync(Results.First().HeaderImg);


        //    var ims = new InMemoryRandomAccessStream();
        //    var dataWriter = new DataWriter(ims);
        //    dataWriter.WriteBytes(contentBytes);
        //    await dataWriter.StoreAsync();
        //    ims.Seek(0);

        //    var bitmap = new BitmapImage();
        //    bitmap.SetSource(ims);

        //    GetHeaderImageUrl();

        //    return bitmap;
        //}
        //public string GetHeaderImageUrl()
        //{
        //    return Results.First().HeaderImg;
        //}


        #endregion
    }



    public class OutputPropertyAlternative
    {
        public OutputPropertyAlternative()
        {
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
