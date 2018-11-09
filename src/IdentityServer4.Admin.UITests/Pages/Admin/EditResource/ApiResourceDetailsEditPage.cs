using IdentityServer4.Admin.UITests.Html.Elements;
using IdentityServer4.Admin.UITests.Html.Elements.Bootstrap;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Pages.Admin.EditResource
{
    internal class ApiResourceDetailsEditPage : SeleniumPage
    {
        internal HtmlInput Name => new HtmlInput(ByXPath("//*[@id='Name']"));

        internal HtmlInput DisplayName => new HtmlInput(ByXPath("//*[@id='DisplayName']"));

        internal HtmlInput Description => new HtmlInput(ByXPath("//*[@id='Description']"));

        internal ToggleSwitch Enabled => new ToggleSwitch(Driver, "//*[@id='Enabled']");

        internal TagPicker UserClaims => new TagPicker(Driver, "//*[@id='api-resource-form']/div[2]/div/div[5]/div/div/div/div/div/input[1]");

        internal HtmlLink ManageApiScopes => new HtmlLink(ByXPath("//*[@id='api-resource-form']/div[2]/div/div[5]/div/a"));

        internal HtmlLink ManageApiSecrets => new HtmlLink(ByXPath("//*[@id='api-resource-form']/div[2]/div/div[6]/div/a"));


        public HtmlElement SaveApiResourceBtn => new HtmlInput(ByXPath("//*[@id='api-resource-save-button']"));

        public HtmlElement DeleteApiResourceBtn => new HtmlInput(ByXPath("//*[@id='api-resource-form']/div[2]/div/div[8]/div/a"));

        internal ApiResourceDetailsEditPage(IWebDriver driver) : base(driver)
        {
        }
    }
}