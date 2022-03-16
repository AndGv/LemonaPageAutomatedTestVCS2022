using Lemona.Drivers;
using Lemona.Page;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace LemonaTest.Test
{
    public class BaseTest
    {
        public static LemonaPage LemonaPage;
        public static LemonaCartPage LemonaCartPage;
        public static LemonaSearchResultPage LemonaSearchResultPage;

        [OneTimeSetUp]
        public static void SetUpOnce()
        {
            IWebDriver Driver = CustomDriver.GetChromeDriver();
            LemonaPage = new LemonaPage(Driver);
            LemonaSearchResultPage = new LemonaSearchResultPage(Driver);
            LemonaCartPage = new LemonaCartPage(Driver);
            LemonaPage.NavigateToPage();
            LemonaPage.WaitAndCloseCookies();
        }

        [SetUp]
        public static void SetUp()
        {
            LemonaPage.NavigateToPage();
            LemonaPage.WaitForPageLoad();
        }

        [TearDown]
        public static void TakeScreeshot()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                LemonaPage.TakeScreenshot();
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            LemonaPage.CloseBrowser();
        }
    }
}
