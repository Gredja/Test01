using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace Locators
{
    public class Tests
    {
        private IWebDriver _driver;
        private IJavaScriptExecutor _js;
        private Actions _mouseAction;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver("C:\\");
        }


        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}