using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Pluto.Test.UI.Api.Admin;
using Pluto.Test.UI.Api.Admin.Login;

namespace Pluto.Test.UI.Steps.Administration
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