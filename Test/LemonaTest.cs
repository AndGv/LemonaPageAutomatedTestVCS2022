using LemonaTest.Test;
using NUnit.Framework;
using System;

namespace Lemona.Test
{
    public class LemonaTest : BaseTest
    {    
        [Test]
        public static void LogIn()
        {
            LemonaPage.LogIn("andre.gvazdauskaite@gmail.com", "Testavimas123.");
            LemonaPage.VerifyLoggedInUser("Test Test1");
        }

        [Test]
        public static void HighestPriceSorting()
        {
            LemonaPage.SearchByText("Belaides Ausines");
            LemonaSearchResultPage.OrderByHighestPrice();
            LemonaSearchResultPage.VerifyDescendingSorting();
        }

        [Test]
        public static void SelectedItemNumberInPage()
        {
            LemonaPage.SearchByText("Belaides Ausines");
            LemonaSearchResultPage.SelectedItemsInPage(20);
            LemonaSearchResultPage.VerifyLoadedItemsInPage(20);
        }

        [Test]
        public static void HighestPriceOfWirelessHeadphones()
        {
            LemonaPage.SearchByText("Belaides Ausines");
            LemonaSearchResultPage.OrderByHighestPrice();
            LemonaSearchResultPage.VerifyHighestProductPrice(new Decimal(79.90));
        }

        [Test]
        public static void ChangeLanguageLTtoRU()
        {
            LemonaPage.ChangeLanguageToRu();
            LemonaPage.VerifyCategoryLabel("КАТЕГОРИИ");
        }

        [Test]
        public static void CartTest()
        {
            LemonaPage.SearchByText("Belaides Ausines");
            LemonaSearchResultPage.AddFirstProductToCart(5);
            LemonaCartPage.NavigateToCart();
            LemonaCartPage.VerifyFinalPrice(new Decimal(148.05));
            LemonaCartPage.VerifyAmount("5");
            LemonaCartPage.RemoveCartItem();
            LemonaCartPage.VerifyEmptyCart();
        }
    }
}
