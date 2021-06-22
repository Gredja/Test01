using System;
using System.Security.Authentication;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Saucedemo.PageObjects
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public HomePage LoginSuccess()
        {
            _driver.Url = "https://www.saucedemo.com/";

           var userNameInput = _driver.FindElement(By.CssSelector("input[data-test='username']"));
            userNameInput.Click();
            userNameInput.SendKeys("standard_user");

            var passwordInput = _driver.FindElement(By.CssSelector("input[data-test='password']"));
            passwordInput.Click();
            passwordInput.SendKeys("secret_sauce");

            var loginButton = _driver.FindElement(By.Id("login-button"));
            loginButton.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(2)).Until(ExpectedConditions.UrlContains("inventory.html"));

            return new HomePage(_driver);
        }
    }
}
