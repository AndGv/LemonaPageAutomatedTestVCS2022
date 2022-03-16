using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Lemona.Page
{
    public class LemonaCartPage : BasePage
    {
        private IWebElement EmptyCartLabel => Driver.FindElement(By.CssSelector(".cart-page-item-list > div"));
        private IWebElement CartButton => Driver.FindElement(By.CssSelector(".dropdown.cart-dropdown > a"));
        private IList<IWebElement> FinalPriceSpans => Driver.FindElements(By.CssSelector("#root > main > div > div > div.cart-page-summary-column.col-lg-3 > div > div > table > tfoot > tr > td:nth-child(2) > span"));
        private IList<IWebElement> CartItems => Driver.FindElements(By.CssSelector(".cart-page-item"));
        private Func<IWebElement, IWebElement> CartItemQuantityField = Item => Item.FindElement(By.CssSelector(".horizontal-quantity"));
        private Func<IWebElement, IWebElement> CartItemRemoveButton = Item => Item.FindElement(By.CssSelector(".product-remove-button"));

        public LemonaCartPage(IWebDriver webDriver) : base(webDriver) { }    
      
        public void NavigateToCart()
        {
            CartButton.Click();
            WaitForCartLoad();
        }

        public void RemoveCartItem()
        {
            CartItemRemoveButton(CartItems[0]).Click();
            WaitForCartRemoveItem();
        }

        public void VerifyFinalPrice(Decimal FinalPrice)
        {
            Assert.AreEqual(FinalPrice, FindFinalPrice());
        }

        public void VerifyAmount(String Quantity)
        {
            Assert.AreEqual(Quantity, CartItemQuantityField(CartItems[0]).GetAttribute("value"));
        }

        public void VerifyEmptyCart()
        {
            Assert.AreEqual("Krepšelis tuščias", EmptyCartLabel.Text);
        }

        public Decimal FindFinalPrice()
        {
            String Price = "";
            foreach (IWebElement Span in FinalPriceSpans)
            {
                Price += Span.Text;
            }
            return Convert.ToDecimal(Price.Substring(1));
        }
    }
}

