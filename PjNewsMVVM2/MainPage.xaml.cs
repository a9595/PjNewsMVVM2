using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PjNewsMVVM2.Data;
using PjNewsMVVM2.Helpers;
using PjNewsMVVM2.Model;
using PjNewsMVVM2.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PjNewsMVVM2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public NewsViewModel MainNewsViewModel
        {
            get { return _news; }
            set { _news = value; }
        }

        private static bool _isDownloaded = false;
        private static NewsViewModel _news = null;

        public MainPage()
        {
            this.InitializeComponent();
            //SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;


            //NewsNEW newNEW = new NewsNEW();
            if (!_isDownloaded && MainNewsViewModel == null)
            {
                MainNewsViewModel = new NewsViewModel();

                _isDownloaded = true;
            }
        }





        private async void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var selectedArticle = sender as ArticleViewModel;
            var selectedArticle = ((sender as ListView).SelectedItem) as ArticleViewModel;

            if (selectedArticle != null)
            {
                var articleLinkUri = new Uri(selectedArticle.Link, UriKind.Absolute);
                //check facebook link
                if (articleLinkUri.Authority == "www.pja.edu.pl")
                {

                    Frame.Navigate(typeof(ArticleView), selectedArticle);
                }
                else //faceboook link for ex
                {
                    await Launcher.LaunchUriAsync(articleLinkUri);

                }

            }
        }
    }
}
