using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Pages.TestClient
{
    internal class CancelledPage : SeleniumPage
    {
        private IWebElement Header => Driver.FindElement(By.TagName("h2"));

        public CancelledPage(IWebDriver driver) : base(driver)
        {
        }

        public void AssertDisplayed()
        {
            Assert.AreEqual("Cancelled", Header.Text, "Should be on clients Authentication Cancelled page.");
        }
    }
}
