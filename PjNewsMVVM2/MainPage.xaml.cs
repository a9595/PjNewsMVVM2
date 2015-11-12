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
        public NewsViewModel MainNewsViewModel { get; set; }
        public MainPage()
        {
            this.InitializeComponent();

            //NewsNEW newNEW = new NewsNEW();
            MainNewsViewModel = new NewsViewModel();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var selectedArticle = sender as ArticleViewModel;
            var selectedArticle = ((sender as ListView).SelectedItem) as ArticleViewModel;

            if (selectedArticle != null)
            {
                textBlockSelectedTitle.Text = selectedArticle.Title;
                Frame.Navigate(typeof(ArticleView), selectedArticle);
            }
        }
    }
}
