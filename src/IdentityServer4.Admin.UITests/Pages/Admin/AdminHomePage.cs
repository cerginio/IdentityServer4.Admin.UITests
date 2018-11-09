using System;
using IdentityServer4.Admin.UITests.Html.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Pluto.Test.UI.Api.Admin
{
    internal class AdminHomePage : SeleniumPage
    {
        internal HtmlLink ClientLink => new HtmlLink(Driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div[1]/div[2]/a")));
        internal HtmlLink IdentityResourceLink => new HtmlLink(Driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div[2]/div[2]/a")));
        internal HtmlLink ApiResourceLink => new HtmlLink(Driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div[3]/div[2]/a")));

        internal AdminHomePage(IWebDriver driver) : base(driver) { }

        public string RootUrl => StsUrl.AdminRootUrl;// "https://localhost:5000/"
     

        public void LongWaitReturnedToApp(int seconds = 3)
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds))
                .Until(d => d.Url.StartsWith(RootUrl));
        }

    }
}
