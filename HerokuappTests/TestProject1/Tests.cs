using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HerokuappTests
{
    public class Tests
    {
        private IWebDriver _driver;
        private IJavaScriptExecutor _js;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver("C:\\");
            _js = (IJavaScriptExecutor)_driver;
        }

        [Test]
        public void AddRemoveElements()
        {
            _driver.Url = "http://the-internet.herokuapp.com/add_remove_elements/";
            _driver.Manage().Window.Maximize();

            var addElementButton = _driver.FindElement(By.XPath("//button[@onclick='addElement()']"));
            addElementButton.Click();
            addElementButton.Click();

            var deleteElementButton = _driver.FindElement(By.XPath("//button[@onclick='deleteElement()']"));
            deleteElementButton.Click();

            var deleteElementButtonsCheckNumber = _driver.FindElements(By.XPath("//button[@onclick='deleteElement()']")).Count;

            Assert.AreEqual(deleteElementButtonsCheckNumber, 1);
        }

        [Test]
        public void Checkboxes()
        {
            _driver.Url = "http://the-internet.herokuapp.com/checkboxes";
            _driver.Manage().Window.Maximize();

            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> checkboxes = _driver.FindElements(By.XPath("//input[@type='checkbox']"));

            var firstCheck = checkboxes[0].GetAttribute("checked");
            Assert.IsNull(firstCheck);

            var secondCheck = checkboxes[1].GetAttribute("checked");
            Assert.NotNull(secondCheck);

            SetAttribute(_js, checkboxes[0], "checked", "true");
            var firstCheckAfterChanged = checkboxes[0].GetAttribute("checked");
            Assert.NotNull(firstCheckAfterChanged);

            RemoveAttribute(_js, checkboxes[1], "checked");
            var secondCheckAfterChanged = checkboxes[1].GetAttribute("checked");
            Assert.IsNull(secondCheckAfterChanged);
        }

        [TearDown]
        public void CloseBrowser()
        {
            //_driver.Quit();
        }

        private void SetAttribute(IJavaScriptExecutor driver, IWebElement element, string attName, string attValue)
        {
            driver.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", element, attName, attValue);
        }

        private void RemoveAttribute(IJavaScriptExecutor driver, IWebElement element, string attName)
        {
            driver.ExecuteScript("arguments[0].removeAttribute(arguments[1]);", element, attName);
        }
    }
}