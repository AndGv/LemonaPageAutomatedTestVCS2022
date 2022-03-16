using NUnit.Framework;
using OpenQA.Selenium;
using System;


namespace Lemona.Page
{
    public class LemonaPage : BasePage
    {
        private IWebElement SearchButton => Driver.FindElement(By.CssSelector("#root > header > div.header-middle > div > div.header-center > div > form > div > button.search-button.btn-with-icon.btn.btn-primary > span"));
        private IWebElement LogInName => Driver.FindElement(By.CssSelector("#root > header > div.header-top > div > div.RightBlock-root-3dY.header-right.header-dropdowns > div > div.header-menu-arrow > a"));
        private IWebElement SearchField => Driver.FindElement(By.CssSelector("div.search-wrapper > input"));
        private IWebElement ButtonLogin => Driver.FindElement(By.CssSelector("#root > header > div.header-top > div > div.RightBlock-root-3dY.header-right.header-dropdowns > ul > li:nth-child(1) > button"));
        private IWebElement EmailAddress => Driver.FindElement(By.CssSelector("body > div.fade.customer-modal-login-wrapper.modal.show > div > div > div > div > form > input"));
        private IWebElement PasswordKey => Driver.FindElement(By.CssSelector("body > div.fade.customer-modal-login-wrapper.modal.show > div > div > div > div > form > div.form-group > div > input"));
        private IWebElement LogInButton => Driver.FindElement(By.CssSelector("body > div.fade.customer-modal-login-wrapper.modal.show > div > div > div > div > form > button.login-form-login-btn.font-group-2.btn.btn-primary"));
        private IWebElement LanguageDropdown => Driver.FindElement(By.CssSelector("#root > header > div.header-top > div > div.header-left.header-dropdowns > div.header-dropdown"));
        private IWebElement RussianLanguage => Driver.FindElement(By.CssSelector("#root > header > div.header-top > div > div.header-left.header-dropdowns > div.header-dropdown > div.header-menu > ul > li:nth-child(2) > button"));
        private IWebElement CategoryLabel => Driver.FindElement(By.CssSelector("#root > header > div.sticky-wrapper > div > div > nav > ul > li.megamenu-container.first-container > div.megamenu-container-link"));
        
        public LemonaPage(IWebDriver webDriver) : base(webDriver) { }    
      
        public void SearchByText(string text)
        {
            SearchField.Clear();
            SearchField.SendKeys(text);
            SearchButton.Click();
            WaitForPageLoad();
        }

        public void LogIn(String email, String password)
        {
            ButtonLogin.Click();  
            EmailAddress.Click();
            EmailAddress.SendKeys(email);
            PasswordKey.Click();
            PasswordKey.SendKeys(password);
            LogInButton.Click();
            WaitForLogin();
        }

        public void VerifyLoggedInUser(String LoggedInUserName)
        {
            Assert.AreEqual(LoggedInUserName, LogInName.Text);
        }

        public void ChangeLanguageToRu()
        {
            LanguageDropdown.Click();
            RussianLanguage.Click();
            WaitForRuPageLoad();
        }

        public void VerifyCategoryLabel(string LabelValue)
        {
            Assert.AreEqual(LabelValue, CategoryLabel.Text);
        }

    }
}

