using IdentityServer4.Admin.UITests.Html.Elements;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Pages.Admin.EditResource.Delete
{
    internal class ApiResourceDeletePage : SeleniumPage
    {
        internal ApiResourceDeletePage(IWebDriver driver) : base(driver)
        {
        }

        internal HtmlInput Name => new HtmlInput(ByXPath("//*[@id='Name']"));


        internal HtmlElement DeleteResource => new HtmlElement(ByXPath("//*[@id='api-resource-form']/div/div/div[2]/div/button"));
    }
}