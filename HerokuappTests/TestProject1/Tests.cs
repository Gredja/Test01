using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace HerokuappTests
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
            _js = (IJavaScriptExecutor)_driver;
            _mouseAction = new Actions(_driver);
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

            var checkboxes = _driver.FindElements(By.XPath("//input[@type='checkbox']"));

            var firstCheck = checkboxes[0].GetAttribute("checked");
            Assert.IsNull(firstCheck);

            var secondCheck = checkboxes[1].GetAttribute("checked");
            Assert.IsNotEmpty(secondCheck);

            SetAttribute(_js, checkboxes[0], "checked", "true");
            var firstCheckAfterChanged = checkboxes[0].GetAttribute("checked");
            Assert.IsNotEmpty(firstCheckAfterChanged);

            RemoveAttribute(_js, checkboxes[1], "checked");
            var secondCheckAfterChanged = checkboxes[1].GetAttribute("checked");
            Assert.IsNull(secondCheckAfterChanged);
        }

        [Test]
        public void Dropdown()
        {
            _driver.Url = "http://the-internet.herokuapp.com/dropdown";
            _driver.Manage().Window.Maximize();

            var dropdown = _driver.FindElement(By.Id("dropdown"));
            Assert.NotNull(dropdown);

            var options = dropdown.FindElements(By.XPath("//option"));
            Assert.NotZero(options.Count);

            SetAttribute(_js, options[1], "selected", "selected");
            var firstSelected = options[1].GetAttribute("selected");
            bool.TryParse(firstSelected, out var result);
            Assert.IsTrue(result);

            SetAttribute(_js, options[2], "selected", "selected");
            var secondSelected = options[2].GetAttribute("selected");
            bool.TryParse(secondSelected, out var result2);
            Assert.IsTrue(result2);
        }

        [Test]
        public void Inputs()
        {
            _driver.Url = "http://the-internet.herokuapp.com/inputs";
            _driver.Manage().Window.Maximize();

            var number = _driver.FindElement(By.XPath("//input[@type='number']"));

            number.SendKeys(Keys.ArrowUp);
            number.SendKeys("989");

            // TODO
        }

        [Test]
        public void SortableDataTables()
        {
            _driver.Url = "http://the-internet.herokuapp.com/tables";
            _driver.Manage().Window.Maximize();

            var table = _driver.FindElement(By.Id("table1"));
            var smith = table.FindElement(By.XPath("//tbody//tr[1]/td[1]")).Text;
            Assert.AreEqual("Smith", smith);

            var email = table.FindElement(By.XPath("//tbody//tr[2]/td[3]")).Text;
            Assert.AreEqual("fbach@yahoo.com", email);
        }

        [Test]
        public async Task Typos()
        {
            _driver.Url = "http://the-internet.herokuapp.com/typos";
            _driver.Manage().Window.Maximize();

            var result = string.Empty;

            var collection = _driver.FindElements(By.XPath("//p"));

            foreach (IWebElement webElement in collection)
            {
                var text = webElement.Text.Replace(" ", "+");
                var requestUrl = $"https://speller.yandex.net/services/spellservice/checkText?text={text}";

                using var client = new HttpClient();
                var response = await client.GetAsync(requestUrl);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
            }

            TestContext.WriteLine(result.Contains("error") ? "Mistake in text" : "Everything is OK");
        }

        [Test]
        public async Task Hovers()
        {
            _driver.Url = "http://the-internet.herokuapp.com/hovers";
            _driver.Manage().Window.Maximize();

            var errors = new List<string>();

            var figures = _driver.FindElements(By.ClassName("figure"));

            foreach (var figure in figures)
            {
                _mouseAction.MoveToElement(figure);
                var figcaption = figure.FindElement(By.ClassName("figcaption"));

                var name = figcaption.FindElement(By.XPath("//h5")).Text;

                TestContext.WriteLine(name);
            }

            //// this makes sure the element is visible before you try to do anything
            //// for slow loading pages
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //var element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id(elementId)));

            //Actions action = new Actions(driver);
            //action.MoveToElement(element).Perform();
        }

        [Test]
        public void NotificationMessage()
        {
            _driver.Url = "http://the-internet.herokuapp.com/notification_message_rendered";
            _driver.Manage().Window.Maximize();

            _driver.FindElement(By.XPath("//*[text()='Click here']")).Click();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
            var isAction = wait.Until(dr => dr.FindElement(By.Id("flash"))).Text.Contains("Action");

            Assert.True(isAction);
        }


        [TearDown]
        public void CloseBrowser()
        {
            _driver.Quit();
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