using OpenQA.Selenium;

namespace Saucedemo.PageObjects
{
    public class CartPage
    {
        private readonly IWebDriver _driver;

        public CartPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string ExpectedPrice { get; set; }

        public bool CheckPrice()
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/cart.html");

            var actualPrice = _driver.FindElement(By.CssSelector(".inventory_item_price")).Text;

            return ExpectedPrice.Equals(actualPrice);
        }
    }
}
