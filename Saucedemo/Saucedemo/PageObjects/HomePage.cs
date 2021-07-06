using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Saucedemo.PageObjects
{
    public class HomePage
    {
        private readonly IWebDriver _driver;

        private readonly By _allProducts = By.CssSelector(".inventory_item");
        private readonly By _productName = By.CssSelector(".inventory_item_name");
        private readonly By _jacket = By.CssSelector(".inventory_item:nth-child(5)");
        private readonly By _price = By.CssSelector(".inventory_item_price ");
        private readonly By _cartButton = By.CssSelector(".btn");
        private readonly By _fullCartLink = By.CssSelector(".shopping_cart_badge");
        private readonly By _sortContainer = By.CssSelector(".product_sort_container");
        private readonly By _productDescription = By.CssSelector(".inventory_item_desc");

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

        public bool Sort(string sortType)
        {
            SelectItem(sortType);

            var original = sortType.Length == 2 ? GetProductNames() : GetProductPrices();
            var copied = Clone(original).ToList();

            copied.Sort();
            if (sortType == Helpers.Helpers.SortType.First(x => x.Key == Helpers.Helpers.Sort.ZtoA).Value ||
                sortType == Helpers.Helpers.SortType.First(x => x.Key == Helpers.Helpers.Sort.HighToLow).Value)
            {
                copied.Reverse();
            }

            return !original.Except(copied).Any();
        }

        public async Task<bool> CheckForTypos(int number)
        {
            var result = string.Empty;
            var text = GetProductDescription(number);
            var requestUrl = $"https://speller.yandex.net/services/spellservice/checkText?text={text}";

            using var client = new HttpClient();
            var response = await client.GetAsync(requestUrl);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = await response.Content.ReadAsStringAsync();
            }

            return !result.Contains("error");
        }

        private IList<string> GetProductNames()
        {
            var products = _driver.FindElements(_allProducts);

            return products.Select(product => product.FindElement(_productName).Text)
                .Where(name => !string.IsNullOrEmpty(name)).ToList();
        }

        private string GetProductDescription(int number)
        {
            var product = _driver.FindElements(_allProducts)[number - 1];
            return product.FindElement(_productDescription).Text;
        }

        private IList<string> GetProductPrices()
        {
            var products = _driver.FindElements(_allProducts);
            var pricesWithDesc = products.Select(product => product.FindElement(_price).Text)
                .Where(name => !string.IsNullOrEmpty(name)).ToList();

            return pricesWithDesc.Select(item => item.Replace("$", string.Empty).Trim()).ToList();
        }

        private void SelectItem(string sortType)
        {
            var sortContainer = new SelectElement(_driver.FindElement(_sortContainer));
            sortContainer.SelectByValue(sortType);
        }

        private IList<T> Clone<T>(IList<T> list)
        {
            return list.ToList();
        }
    }
}
