using NUnit.Framework;
using OpenQA.Selenium;

namespace Saucedemo.PageObjects
{
    public class HomePage
    {
        private readonly IWebDriver _driver;

        private By _jacket = By.CssSelector(".inventory_item:nth-child(5)");
        private By _price = By.CssSelector(".inventory_item_price ");
        private By _cartButton = By.CssSelector(".btn");
        private By _fullCartLink = By.CssSelector(".shopping_cart_badge");

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void BuyProduct()
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
               }
               else
               {
                   Assert.Fail("Can not buy products");
                }

            }

        }
    }
}
