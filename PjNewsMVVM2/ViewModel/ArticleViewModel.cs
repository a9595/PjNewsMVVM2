using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Interop;
using PjNewsMVVM2.Data;
using Prism.Mvvm;

namespace PjNewsMVVM2.ViewModel
{
    class ArticleViewModel : BindableBase
    {
        private ArticleResult _articleResult;

        public ArticleViewModel()
        {
            _articleResult = new ArticleResult();

        }

        public ArticleResult ArticleResult
        {
            get { return this._articleResult; }
            set { SetProperty(ref this._articleResult, value); }
        }

    }
}
