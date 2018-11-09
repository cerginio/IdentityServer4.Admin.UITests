using IdentityServer4.Admin.UITests.Html.Elements;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Pages.Admin.Base
{
    internal class TabBase : SeleniumPage
    {
        private readonly string _tabPath;
        public HtmlElement Header => new HtmlInput(ByXPath(_tabPath));

        public TabBase(IWebDriver driver, string tabPath) : base(driver)
        {
            _tabPath = tabPath;
        }
    }
}