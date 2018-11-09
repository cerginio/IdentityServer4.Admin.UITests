using IdentityServer4.Admin.UITests.Html.Elements;
using OpenQA.Selenium;

namespace Pluto.Test.UI.Api.Admin.EditClient.Delete
{
    internal class ClientSecretDeletePage : SeleniumPage
    {
        internal ClientSecretDeletePage(IWebDriver driver) : base(driver)
        {
        }

        internal HtmlInput ClientType => new HtmlInput(ByXPath("//*[@id='Type']"));

        internal HtmlInput ClientValue => new HtmlInput(ByXPath("//*[@id='Value']"));

        internal HtmlElement DeleteClientClaim => new HtmlElement(ByXPath("/html/body/div[2]/form/div/div/div/div/div[3]/div/button"));
    }
}