using IdentityServer4.Admin.UITests.Pages.Admin;
using IdentityServer4.Admin.UITests.Pages.Admin.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Steps.Administration
{
    public class AdminStepsBase
    {
        protected readonly string _adminRoot;

        protected IWebDriver Driver;

        public AdminStepsBase(IWebDriver driver, string adminRoot)
        {
            Driver = driver;
            _adminRoot = adminRoot;
        }

        internal AdminHomePage GoToHomePage()
        {
            Driver.Navigate().GoToUrl(_adminRoot);

            AdministartorLocalLoginPage login = new AdministartorLocalLoginPage(Driver);
            login.DoLogin();

            AdminHomePage home = new AdminHomePage(Driver);
            home.LongWaitReturnedToApp(5);

            Assert.IsTrue(home.IsOnPage(home.RootUrl));
            return home;
        }
    }
}