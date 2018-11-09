using IdentityServer4.Admin.UITests.Html.Elements;
using IdentityServer4.Admin.UITests.Html.Elements.Bootstrap;
using IdentityServer4.Admin.UITests.Pages.Admin.Base;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Pages.Admin.EditClient.Tabs
{
    internal class ConsentScreenTab : TabBase
    {
        internal ToggleSwitch RequireConsent => new ToggleSwitch(Driver, "//*[@id='RequireConsent']");
        internal ToggleSwitch AllowRememberConsent => new ToggleSwitch(Driver, "//*[@id='AllowRememberConsent']");

        internal HtmlInput ClientUri => new HtmlInput(ByXPath("//*[@id='ClientUri']"));
        internal HtmlInput LogoUri => new HtmlInput(ByXPath("//*[@id='LogoUri']"));

        public ConsentScreenTab(IWebDriver driver, string tabPath) : base(driver, tabPath)
        {
        }

        public ConsentScreenTab(IWebDriver driver) : base(driver, "//*[@id='nav-consent-tab']")
        {
            Header.Click();
        }
    }
}