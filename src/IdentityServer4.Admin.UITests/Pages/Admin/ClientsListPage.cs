using OpenQA.Selenium;
using Pluto.Test.UI.Api.Admin;

namespace IdentityServer4.Admin.UITests.Pages.Admin
{
    internal class ClientsListPage : SearchListPageBase
    {
        internal ClientsListPage(IWebDriver driver) : base(driver)
        {
        }
    }
}
