﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
            ArticleViewModel article = e.Parameter as ArticleViewModel;

            TextBlockTitle.Text = article.Title;

            //var article2 = NewsGrabber.GetAlternativeArticleSimply(article.Link);
            //var article2 = NewsGrabber.GetAlternativeArticleSimply(article.Link);
            //string content = article2.Results.First().Content;
            //string content = "content";

            var blocks = await ArticleContentDownloader.GetRichTextBoxContentByLink(article.Link);

            RichContent.Blocks.Clear();
            foreach (Block b in blocks)
                RichContent.Blocks.Add(b);

            //fillContent(content);

            base.OnNavigatedTo(e);
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
    }
}
