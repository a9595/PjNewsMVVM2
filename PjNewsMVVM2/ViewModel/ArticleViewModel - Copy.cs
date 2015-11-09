using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Interop;
using PjNewsMVVM2.Data;
using PjNewsMVVM2.Model;
using Prism.Mvvm;

namespace PjNewsMVVM2.ViewModel
{
    class NewsViewModel : BindableBase
    {
        private News _news;

        public NewsViewModel()
        {
            _news = new News();

        }

        public News News {
            get { return _news; }
            set { SetProperty(ref _news, value);  }
        }
       

    }
}
