using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using Saucedemo.Helpers;
using Saucedemo.PageObjects;
using Saucedemo.PageObjects.Base;

namespace Saucedemo
{
    [TestFixture]
    [AllureNUnit]
    public class Tests : BaseTest
    {
        [Test, Description("Test_CheckCart_SameProductsInCart"), TestCaseSource(typeof(TestSources), "AddCredentials")]
        [AllureTag("CI")]
        [AllureSeverity(SeverityLevel.critical)]
        public void Test_CheckCart_SameProductsInCart(string login, string password)
        {
            _logger.Debug($"Test {nameof(Test_CheckCart_SameProductsInCart)} with parameters: login-{login}, password-{password} started");

            var homePage = new LoginPage(_driver).LoginSuccess(login, password);
            var cartPage = homePage?.BuyProduct();
            var result = cartPage?.CheckPrice();

            Assert.IsTrue(result);

            _logger.Debug($"Test {nameof(Test_CheckCart_SameProductsInCart)} finished");
        }

        [Test, Description("Test_RightSort"), TestCaseSource(typeof(TestSources), "AddCredentialsAndSortType")]
        [AllureTag("CI")]
        [AllureSeverity(SeverityLevel.normal)]
        public void Test_RightSort(string login, string password, string sortType)
        {
            _logger.Debug($"Test {nameof(Test_RightSort)} with parameters: login-{login}, password-{password}, sortType-{sortType} started");

            var homePage = new LoginPage(_driver).LoginSuccess(login, password);
            Assert.IsTrue(homePage.Sort(sortType));

            _logger.Debug($"Test {nameof(Test_RightSort)} finished");
        }

        [Test, Description("Test_ProductDescriptionTyposCheck"), TestCaseSource(typeof(TestSources), "AddProductDescription")]
        [AllureTag("CI")]
        [AllureSeverity(SeverityLevel.critical)]
        public void Test_ProductDescriptionTyposCheck(int number)
        {
            _logger.Debug($"Test {nameof(Test_ProductDescriptionTyposCheck)} with parameters: number-{number} started");

            var homePage = new LoginPage(_driver).LoginSuccess("standard_user", "secret_sauce");
            Assert.IsTrue(homePage.CheckForTypos(number).Result);

            _logger.Debug($"Test {nameof(Test_ProductDescriptionTyposCheck)} finished");
        }

        [TearDown]
        public void Close()
        {
            _driver?.Close();
            _driver?.Quit();
        }
    }
}