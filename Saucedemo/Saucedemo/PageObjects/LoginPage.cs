using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Saucedemo.PageObjects
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public HomePage LoginSuccess(string login, string password)
        {
            _driver.Url = "https://www.saucedemo.com/";

            var userNameInput = _driver.FindElement(By.CssSelector("input[data-test='username']"));
            userNameInput.Click();
            userNameInput.SendKeys(login);

            var passwordInput = _driver.FindElement(By.CssSelector("input[data-test='password']"));
            passwordInput.Click();
            passwordInput.SendKeys(password);

            var loginButton = _driver.FindElement(By.Id("login-button"));
            loginButton.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(2)).Until(ExpectedConditions.UrlContains("inventory.html"));

            return new HomePage(_driver);
        }
    }
}
