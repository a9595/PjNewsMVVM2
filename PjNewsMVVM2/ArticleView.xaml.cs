using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ArticleViewModel article = e.Parameter as ArticleViewModel;

            TextBlockTitle.Text = article.Title;
            
            var article2 = NewsGrabber.GetAlternativeArticleSimply(article.Link);
            string content = article2.Results.First().Content;
            
            fillContent(content);
            
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
