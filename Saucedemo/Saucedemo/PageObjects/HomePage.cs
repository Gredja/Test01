using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Saucedemo.PageObjects
{
    public class HomePage
    {
        private readonly IWebDriver _driver;

        private readonly By _jacket = By.CssSelector(".inventory_item:nth-child(5)");
        private readonly By _price = By.CssSelector(".inventory_item_price ");
        private readonly By _cartButton = By.CssSelector(".btn");
        private readonly By _fullCartLink = By.CssSelector(".shopping_cart_badge");

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public CartPage BuyProduct()
        {
            var item = _driver.FindElement(_jacket);

            if (item != null)
            {
                var price = item.FindElement(_price).Text;
                item.FindElement(_cartButton).Click();

                var cart = _driver.FindElement(_fullCartLink);
                var productsQuantity = cart.Text;

                if (!string.IsNullOrEmpty(productsQuantity))
                {
                    cart.Click();
                    new WebDriverWait(_driver, TimeSpan.FromSeconds(2)).Until(ExpectedConditions.UrlContains("cart.html"));

                    var cartPage = new CartPage(_driver)
                    {
                        ExpectedPrice = price
                    };
                    return cartPage;
                }

                throw new Exception("Can not buy products");
            }

            throw new Exception("Can not buy products");
        }
    }
}
