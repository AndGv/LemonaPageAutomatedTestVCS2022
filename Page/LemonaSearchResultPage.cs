using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lemona.Page
{
    public class LemonaSearchResultPage : BasePage
    {
        private SelectElement OrderByAmountDropdown => new SelectElement(Driver.FindElement(By.CssSelector("#root > main > div > div.container > div > div > nav.toolbox.product-list-options-display-options > div.product-list-options-toolbox-wrapper > div.product-list-options-dropdown-wrapper > div.product-count.product-list-dropdown.toolbox-item.toolbox-sort > form > div > select")));
        private IList<IWebElement> ProductList => Driver.FindElements(By.CssSelector(".product-list.row .product-list-product"));
        private SelectElement OrderByDropdown => new SelectElement(Driver.FindElement(By.CssSelector("#root > main > div > div.container > div > div > nav.toolbox.product-list-options-display-options > div.product-list-options-toolbox-wrapper > div.product-list-options-dropdown-wrapper > div:nth-child(3) > form > div > select")));
        private IWebElement FirstProductElement => Driver.FindElement(By.CssSelector("#root > main > div > div.container > div > div > div.product-list.row > div:nth-child(2)"));
        private Func<IWebElement, IList<IWebElement>> ProductPriceSpans = Product => Product.FindElements(By.CssSelector(".price-box .price-box-price span"));
        private Func<IWebElement, IWebElement> ProductAddToCartButton = Product => Product.FindElement(By.CssSelector(".btn-add-cart"));
        private Func<IWebElement, IWebElement> ProductCartQuantityInput = Product => Product.FindElement(By.CssSelector(".horizontal-quantity"));

        public LemonaSearchResultPage(IWebDriver Driver) : base(Driver) { }

        public void SelectedItemsInPage(int itemNumber)
        {
            OrderByAmountDropdown.SelectByValue(itemNumber.ToString());
            WaitForPageLoad();
        }

        public void AddFirstProductToCart(int Quantity)
        {
            ProductCartQuantityInput(FirstProductElement).Clear();
            ProductCartQuantityInput(FirstProductElement).SendKeys(Quantity.ToString());
            ProductAddToCartButton(FirstProductElement).Click();
            WaitForCartAdd();
        }

        public void VerifyLoadedItemsInPage(int ExpectedNumberOfItems)
        {
            Assert.AreEqual(ExpectedNumberOfItems, ProductList.Count);
        }

        public void OrderByHighestPrice()
        {
            OrderByDropdown.SelectByValue("price_desc");
            WaitForPageLoad();
        }

        public void VerifyHighestProductPrice(Decimal ProductPrice)
        {
            Assert.AreEqual(ProductPrice, FindFirstEelementPrice());
        }

        public void VerifyDescendingSorting()
        {
            List<Decimal> PriceList = FindItemsPriceList().OrderByDescending(x => x).ToList();
            Assert.AreEqual(PriceList, FindItemsPriceList());
        }

        public Decimal FindFirstEelementPrice()
        {
            return FindItemPrice(FirstProductElement);
        }

        public List<Decimal> FindItemsPriceList()
        {
            List<Decimal> PriceList = new List<Decimal>();
            foreach (IWebElement Product in ProductList)
            {
                PriceList.Add(FindItemPrice(Product));
            }
            return PriceList;
        }

        public Decimal FindItemPrice(IWebElement Product)
        {
            String Price = "";
            foreach (IWebElement Span in ProductPriceSpans(Product))
            {
                Price += Span.Text;
            }
            return Convert.ToDecimal(Price.Substring(1));
        }
    }
}

