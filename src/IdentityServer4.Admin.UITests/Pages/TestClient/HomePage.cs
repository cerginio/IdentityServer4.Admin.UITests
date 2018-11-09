using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Pluto.Test.UI.Api.TestCoreMvc
{
    internal class HomePage : SeleniumPage
    {
        private IWebElement SignInLink => Driver.FindElement(By.ClassName("navbar-right")).FindElement(By.TagName("a"));

        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public bool IsSignedIn
        {
            get
            {
                try
                {
                    return SignInLink.Text == "Sign Out";
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public void LongWaitReturnedToApp(int seconds = 10)
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds))
                .Until(d => d.Url.StartsWith(TestClientUrl.RootUrl));
        }
    }
}
