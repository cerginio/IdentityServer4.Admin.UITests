using IdentityServer4.Admin.UITests.Html.Elements;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Pages.Admin.EditClient.Pages
{
    internal class ClientRegistrationPage : SeleniumPage
    {
        internal HtmlInput ClientId => new HtmlInput(ByXPath("//*[@id='ClientId']"));

        internal HtmlInput ClientName => new HtmlInput(ByXPath("//*[@id='ClientName']"));

        internal HtmlElement SaveClient => new HtmlElement(ByXPath("//*[@id='client-form']/div[3]/div/button"));


        internal HtmlElement EmptyTemplate => new HtmlElement(ByXPath("//*[@id='nav-name']/div/div/div[3]/div[1]"));
        internal HtmlElement WebAppServerImplicitTemplate => new HtmlElement(ByXPath("//*[@id='nav-name']/div/div/div[3]/div[2]"));
        internal HtmlElement WebAppServerHybridTemplate => new HtmlElement(ByXPath("//*[@id='nav-name']/div/div/div[3]/div[3]"));
        internal HtmlElement SPAImplicitHybridTemplate => new HtmlElement(ByXPath("//*[@id='nav-name']/div/div/div[4]/div[1]"));
        internal HtmlElement MobileHybridTemplate => new HtmlElement(ByXPath("//*[@id='nav-name']/div/div/div[4]/div[2]"));
        internal HtmlElement MachineTemplate => new HtmlElement(ByXPath("//*[@id='nav-name']/div/div/div[4]/div[3]"));


        internal ClientRegistrationPage(IWebDriver driver) : base(driver)
        {
        }
    }
}