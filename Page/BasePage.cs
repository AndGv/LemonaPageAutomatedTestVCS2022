using Lemona.Tools;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Lemona.Page
{
    public class BasePage
    {
        protected IWebDriver Driver;
        private static readonly String PageAddress = "https://www.lemona.lt/";

        public BasePage(IWebDriver Driver)
        {
            this.Driver = Driver;
        }

        public void CloseBrowser()
        {
            Driver.Quit();
        }

        public void NavigateToPage()
        {
            Driver.Navigate().GoToUrl(PageAddress);
            WaitForPageLoad();
        }

        public void WaitForCartAdd()
        {
            WebDriverWait CartWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            CartWait.Until(Driver => Driver.FindElement(By.CssSelector(".mini-cart-desktop")).Displayed);
        }

        public void WaitForCartLoad()
        {
            WebDriverWait CartWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            CartWait.Until(Driver => Driver.FindElement(By.CssSelector(".cart-page-item-list")).Displayed);
        }

        public void WaitForCartRemoveItem()
        {
            WebDriverWait CartWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            CartWait.Until(Driver => Driver.FindElement(By.CssSelector(".cart-page-item-list > div")).Text.Equals("Krepšelis tuščias"));
        }

        public void WaitAndClosePopUp()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
            wait.Until(Driver => Driver.FindElement(By.CssSelector("#soundest-forms-container a.soundest-form-background-image-close")).Displayed);
            Driver.FindElement(By.CssSelector("#soundest-forms-container a.soundest-form-background-image-close")).Click();
        }

        public void WaitAndCloseCookies()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.FindElement(By.CssSelector("#root > div.CookieWarning-warningContainer-2rh > div > button")).Displayed);
            IWebElement cookieButton = Driver.FindElement(By.CssSelector("#root > div.CookieWarning-warningContainer-2rh > div > button"));
            cookieButton.Click();
        }

        public void WaitForPageLoad()
        {
            WebDriverWait SearchFieldWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            SearchFieldWait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#root > header > div.header-middle > div > div.header-center > div > form > div > div.search-wrapper > input")));
            WebDriverWait spinnerWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            spinnerWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(".Spinner-wrapper-Htm")));
        }

        public void WaitForLogin()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("#root > header > div.header-top > div > div.RightBlock-root-3dY.header-right.header-dropdowns > div > div.header-menu-arrow > a")));
        }

        public void WaitForRuPageLoad()
        {
            WebDriverWait categoryWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            categoryWait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#root > header > div.sticky-wrapper > div > div > nav > ul > li.megamenu-container.first-container > div.megamenu-container-link")));
        }

        public void TakeScreenshot()
        {
            MyScreenshots.MakeScreeshot(Driver);
        }

    }
}
