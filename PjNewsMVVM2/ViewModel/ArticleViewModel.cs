using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Interop;
using PjNewsMVVM2.Data;
using PjNewsMVVM2.Helpers;
using Prism.Mvvm;

namespace PjNewsMVVM2.ViewModel
{
    class ArticleViewModel : BindableBase
    {
        private ArticleNEW _article;

        public ArticleViewModel()
        {
            _articleResult = new ArticleNEW();

        }

        public ArticleResult ArticleResult
        {
            get { return this._articleResult; }
            set { SetProperty(ref this._articleResult, value); }
        }

    }
}
