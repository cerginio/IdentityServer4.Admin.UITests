using IdentityServer4.Admin.UITests.Html.Elements;
using OpenQA.Selenium;
using Pluto.Test.UI.Api.Admin.Base;

namespace Pluto.Test.UI.Api.Admin.EditClient.Tabs
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