using IdentityServer4.Admin.UITests.Html.Elements;
using OpenQA.Selenium;

namespace Pluto.Test.UI.Api.Admin.EditClient.Delete
{
    internal class ClientDeletePage : SeleniumPage
    {
        internal ClientDeletePage(IWebDriver driver) : base(driver)
        {
        }

        internal HtmlInput ClientId => new HtmlInput(ByXPath("//*[@id='ClientId']"));

        internal HtmlInput ClientName => new HtmlInput(ByXPath("//*[@id='ClientName']"));

        internal HtmlElement DeleteClient => new HtmlElement(ByXPath("//*[@id='client-form']/div/div/div[3]/div/button"));
    }
}