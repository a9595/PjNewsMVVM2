using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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


            //News newNEW = new News();
            if (!_isDownloaded && MainNewsViewModel == null)
            {
                MainNewsViewModel = new NewsViewModel();

                _isDownloaded = true;
            }
        }

        private async void MainList_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedArticle = e.ClickedItem as ArticleViewModel;
            //var selectedArticle = ((sender as ListView).SelectedItem) as ArticleViewModel;

            if (selectedArticle == null) return;

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

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_isDownloaded)
            {
                if (MainList != null) MainList.SelectedIndex = -1;

                TextBlockLoading.Visibility = Visibility.Collapsed;
            }
        }

        private async void OnLoading(FrameworkElement sender, object args)
        {
            MainNewsViewModel.LoadCachedData();
            try
            {
                await MainNewsViewModel.DownloadNews();
            }
            catch (Exception ex)
            {
                TextBlockLoading.Text = "No internet connection";
                _isDownloaded = false;
                TextBlockLoading.Visibility = Visibility.Visible;
            }
        }

    }
}

