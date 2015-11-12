using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.Storage;

namespace PjNewsMVVM2.Assets
{
    public class UserDB
    {
        // actual data to be preserved for each user
        public int A;
        public string Z;

        // metadata        
        public DateTime LastSaved;
        public int eon;

        private string dbpath;


        public static async Task<string> Kurwa()
        {
            //ResourceContext.SetGlobalQualifierValue("dxfeaturelevel", combo.SelectedValue.ToString());
            string a = @"C:\VisualStudioFiles\Projects\PjNewsMVVM2\PjNewsMVVM2\Assets\DownloadedNews.json";

            //Uri uri = new Uri("ms-appx:///Assets/DownloadedNews.json");
            Uri uri = new Uri(a);

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            string content = await FileIO.ReadTextAsync(file);
            return content;
        }

        //
        //        public static UserDB Load(string path)
        //        {
        //            Uri path;
        ////            StorageFolder appFolder =
        ////               Windows.ApplicationModel.Package.Current.InstalledLocation;
        //
        //            Uri shader = new Uri(baseUri, "shaders/test.ps");
        //            UserDB udb;
        //            try
        //            {
        //                System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(UserDB));
        //                using (System.IO.StreamReader reader = System.IO.File.OpenText(path))
        //                {
        //                    udb = (UserDB)s.Deserialize(reader);
        //                }
        //            }
        //            catch
        //            {
        //                udb = new UserDB();
        //            }
        //            udb.dbpath = path;
        //
        //            return udb;
        //        }




    }
}

