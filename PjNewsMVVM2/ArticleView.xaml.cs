using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PjNewsMVVM2.Data;
using PjNewsMVVM2.Helpers;
using PjNewsMVVM2.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PjNewsMVVM2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ArticleView : Page
    {
        private ArticleViewModel _article;

        public ArticleView()
        {
            this.InitializeComponent();
            //SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
            //{
            //    //Frame.Navigate(typeof(ArticleView), selectedArticle);
            //    Frame.Navigate(typeof(MainPage));
            //};

            BackButtonActivate();
        }

        private void BackButtonActivate()
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                AppViewBackButtonVisibility.Visible;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                Debug.WriteLine("BackRequested");
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    a.Handled = true;
                }
            };
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _article = e.Parameter as ArticleViewModel;

            await DownloadArticle();


            //fillContent(content);

            base.OnNavigatedTo(e);
        }

        private async Task DownloadArticle()
        {
            if (_article != null)
            {
                TextBlockTitle.Text = _article.Title;

                //check facebook link:
                var articleLinkUri = new Uri(_article.Link, UriKind.Absolute);
                if (articleLinkUri.Authority == "www.pja.edu.pl")
                {
                    try
                    {
                        await FillRTB(_article);
                    }
                    catch (Exception)
                    {
                        TextBlockLoading.Text = "Can't load the page";
                    }
                }
            }
        }

        private async Task FillRTB(ArticleViewModel article)
        {
            var blocks = await ArticleContentDownloader.GetRichTextBoxContentByLink(article.Link);


            RichContent.Blocks.Clear();
            foreach (Block b in blocks)
                RichContent.Blocks.Add(b);
            TextBlockLoading.Visibility = Visibility.Collapsed;
        }

        private void fillContent(string Content)
        {
            // Create a Run of plain text and some bold text.
            Run myRun1 = new Run();
            myRun1.Text = Content;


            // Create a paragraph and add the Run and Bold to it.
            Paragraph myParagraph = new Paragraph();
            myParagraph.Inlines.Add(myRun1);

            // Add the paragraph to the RichTextBox.
            RichContent.Blocks.Add(myParagraph);

        }

        private async void AppBarButtonRefresh_OnClick(object sender, RoutedEventArgs e)
        {
            TextBlockLoading.Visibility = Visibility.Visible; // loading page... show

            await DownloadArticle();
        }

        private async void AppBarButtonGoToLink_OnClick(object sender, RoutedEventArgs e)
        {
            if (_article == null) return;

            Uri articleLinkUri = new Uri(_article.Link, UriKind.Absolute);

            await Launcher.LaunchUriAsync(articleLinkUri);
        }
    }
}
