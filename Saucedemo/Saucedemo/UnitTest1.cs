using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Saucedemo.PageObjects;

namespace Saucedemo
{
    [TestFixture]
    public class Tests
    {
        private static IWebDriver _driver;
        private static readonly By _allProducts = By.CssSelector(".inventory_item");

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }

        [Test, TestCaseSource("AddCredentials")]
        public void Test_CheckCart_SameProductsInCart(string login, string password)
        {
            var homePage = new LoginPage(_driver).LoginSuccess(login, password);
            var cartPage = homePage?.BuyProduct();
            var result = cartPage?.CheckPrice();

            Assert.IsTrue(result);
        }

        [Test, TestCaseSource("AddCredentialsAndSortType")]
        public void Test_RightSort(string login, string password, string sortType)
        {
            var homePage = new LoginPage(_driver).LoginSuccess(login, password);
            Assert.IsTrue(homePage.Sort(sortType));
        }

        [Test, TestCaseSource("AddProductDescription")]
        public void Test_ProductDescriptionTyposCheck(int number)
        {
            var homePage = new LoginPage(_driver).LoginSuccess("standard_user", "secret_sauce");
            Assert.IsTrue(homePage.CheckForTypos(number).Result);
        }

        [TearDown]
        public void Close()
        {
            _driver?.Close();
            _driver?.Quit();
        }

        private static IEnumerable<TestCaseData> AddProductDescription()
        {
            for (var i = 1; i <= 6; i++)
            {
                yield return new TestCaseData(i);
            }
        }

        private static IEnumerable<TestCaseData> AddCredentials()
        {
            yield return new TestCaseData("standard_user", "secret_sauce");
            yield return new TestCaseData("problem_user", "secret_sauce");
            yield return new TestCaseData("performance_glitch_user", "secret_sauce");
        }

        private static IEnumerable<TestCaseData> AddCredentialsAndSortType()
        {
            yield return new TestCaseData("standard_user", "secret_sauce", 
                Helpers.Helpers.SortType.First(x => x.Key == Helpers.Helpers.Sort.AtoZ).Value);
            yield return new TestCaseData("problem_user", "secret_sauce", 
                Helpers.Helpers.SortType.First(x => x.Key == Helpers.Helpers.Sort.AtoZ).Value);
            yield return new TestCaseData("performance_glitch_user", "secret_sauce", 
                Helpers.Helpers.SortType.First(x => x.Key == Helpers.Helpers.Sort.AtoZ).Value);

            yield return new TestCaseData("standard_user", "secret_sauce",
                Helpers.Helpers.SortType.First(x => x.Key == Helpers.Helpers.Sort.ZtoA).Value);
            yield return new TestCaseData("problem_user", "secret_sauce",
                Helpers.Helpers.SortType.First(x => x.Key == Helpers.Helpers.Sort.ZtoA).Value);
            yield return new TestCaseData("performance_glitch_user", "secret_sauce",
                Helpers.Helpers.SortType.First(x => x.Key == Helpers.Helpers.Sort.ZtoA).Value);

            yield return new TestCaseData("standard_user", "secret_sauce",
                Helpers.Helpers.SortType.First(x => x.Key == Helpers.Helpers.Sort.LowToHigh).Value);
            yield return new TestCaseData("problem_user", "secret_sauce",
                Helpers.Helpers.SortType.First(x => x.Key == Helpers.Helpers.Sort.LowToHigh).Value);
            yield return new TestCaseData("performance_glitch_user", "secret_sauce",
                Helpers.Helpers.SortType.First(x => x.Key == Helpers.Helpers.Sort.LowToHigh).Value);

            yield return new TestCaseData("standard_user", "secret_sauce",
                Helpers.Helpers.SortType.First(x => x.Key == Helpers.Helpers.Sort.HighToLow).Value);
            yield return new TestCaseData("problem_user", "secret_sauce",
                Helpers.Helpers.SortType.First(x => x.Key == Helpers.Helpers.Sort.HighToLow).Value);
            yield return new TestCaseData("performance_glitch_user", "secret_sauce",
                Helpers.Helpers.SortType.First(x => x.Key == Helpers.Helpers.Sort.HighToLow).Value);
        }
    }
}