using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Saucedemo.PageObjects;

namespace Saucedemo
{
    public class Tests
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
        }

        [Test]
        public void CheckCart()
        {
            var homePage = new LoginPage(_driver).LoginSuccess();
            var cartPage = homePage?.BuyProduct();
            var result = cartPage.CheckPrice();

            Assert.IsTrue(result);
        }

        [TearDown]
        public void Close()
        {
            _driver?.Close();
        }
    }
}