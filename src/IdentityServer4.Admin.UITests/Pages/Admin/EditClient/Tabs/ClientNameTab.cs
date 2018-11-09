using IdentityServer4.Admin.UITests.Html.Elements;
using IdentityServer4.Admin.UITests.Pages.Admin.Base;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Pages.Admin.EditClient.Tabs
{
    internal class ClientNameTab : TabBase
    {
        internal HtmlInput ClientId => new HtmlInput(ByXPath("//*[@id='ClientId']"));

        internal HtmlInput ClientName => new HtmlInput(ByXPath("//*[@id='ClientName']"));

        internal ClientNameTab(IWebDriver driver) : base(driver, "//*[@id='nav-name-tab']")
        {
        }  
    
    }
}