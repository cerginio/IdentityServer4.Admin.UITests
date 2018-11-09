using IdentityServer4.Admin.UITests.Html.Elements;
using OpenQA.Selenium;

namespace Pluto.Test.UI.Api.Admin.EditResource.Delete
{
    internal class IdentityResourceDeletePage : SeleniumPage
    {
        internal IdentityResourceDeletePage(IWebDriver driver) : base(driver)
        {
        }

        internal HtmlInput Name => new HtmlInput(ByXPath("//*[@id='Name']"));

        internal HtmlElement DeleteResource => new HtmlElement(ByXPath("/html/body/div[2]/form/div/div/div[2]/div/button"));
    }
}