using IdentityServer4.Admin.UITests.Html.Elements;
using IdentityServer4.Admin.UITests.Html.Elements.Bootstrap;
using OpenQA.Selenium;
using Pluto.Test.UI.Api.Admin.Base;

namespace Pluto.Test.UI.Api.Admin.EditClient.Tabs
{
    internal class AuthenticationLogoutTab : TabBase
    {

        internal HtmlInput FrontChannelLogoutUri => new HtmlInput(ByXPath("//*[@id='FrontChannelLogoutUri']"));
        internal HtmlInput BackChannelLogoutUri => new HtmlInput(ByXPath("//*[@id='BackChannelLogoutUri']"));


        internal ToggleSwitch FrontChannelLogoutSessionRequired => new ToggleSwitch(Driver, "//*[@id='FrontChannelLogoutSessionRequired']");
        internal ToggleSwitch BackChannelLogoutSessionRequired => new ToggleSwitch(Driver, "//*[@id='BackChannelLogoutSessionRequired']");
        internal ToggleSwitch EnableLocalLogin => new ToggleSwitch(Driver, "//*[@id='EnableLocalLogin']");


        internal TagPicker PostLogoutRedirectUris  => new TagPicker(Driver, "//*[@id='nav-authentication']/div/div/div[6]/div/div/div/div/div/input[1]");
        internal TagPicker IdentityProviderRestrictions => new TagPicker(Driver, "//*[@id='nav-authentication']/div/div/div[7]/div/div/div/div/div/input[1]");

    

        public AuthenticationLogoutTab(IWebDriver driver, string tabPath) : base(driver, tabPath)
        {
        }

        public AuthenticationLogoutTab(IWebDriver driver) : base(driver, "//*[@id='nav-authentication-tab']")
        {
            Header.Click();
        }
    }
}
