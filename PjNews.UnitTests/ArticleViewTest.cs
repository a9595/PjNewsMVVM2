using NUnit.Framework;
using PjNewsMVVM2.ViewModels;

namespace PjNews.UnitTests
{
    class ArticleViewTest
    {
        [Test]
        public void ConstructorTest()
        {
            //arrange
            //act 

            string date = "28.08.15";
            string link = "link";
            string title = "title";
            ArticleViewModel articleViewModel = new ArticleViewModel(date, link, title);

            Assert.AreEqual(date, articleViewModel.Date);
            Assert.AreEqual(link, articleViewModel.Link);
            Assert.AreEqual(title, articleViewModel.Title);
            
        }
    }
}
