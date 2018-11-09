using IdentityServer4.Admin.UITests.Html.Elements;
using IdentityServer4.Admin.UITests.Html.Elements.Bootstrap;
using OpenQA.Selenium;

namespace Pluto.Test.UI.Api.Admin.EditResource
{
    internal class IdentityResourceDetailsEditPage : SeleniumPage
    {
        internal HtmlInput Name => new HtmlInput(ByXPath("//*[@id='Name']"));

        internal HtmlInput DisplayName => new HtmlInput(ByXPath("//*[@id='DisplayName']"));

        internal HtmlInput Description => new HtmlInput(ByXPath("//*[@id='Description']"));

        internal ToggleSwitch Enabled => new ToggleSwitch(Driver, "//*[@id='Enabled']");
        internal ToggleSwitch ShowInDiscoveryDocument => new ToggleSwitch(Driver, "//*[@id='ShowInDiscoveryDocument']");
        internal ToggleSwitch Required => new ToggleSwitch(Driver, "//*[@id='Required']");
        internal ToggleSwitch Emphasize => new ToggleSwitch(Driver, "//*[@id='Emphasize']");

        internal TagPicker UserClaims => new TagPicker(Driver, "//*[@id='identity-resource-form']/div[2]/div/div[8]/div/div/div/div/div/input[1]");


        public HtmlElement SaveResourceBtn => new HtmlInput(ByXPath("//*[@id='identity-resource-save-button']"));

        public HtmlElement DeleteResourceBtn => new HtmlInput(ByXPath("//*[@id='identity-resource-form']/div[2]/div/div[9]/div/a"));



        internal IdentityResourceDetailsEditPage(IWebDriver driver) : base(driver)
        {
        }
    }
}