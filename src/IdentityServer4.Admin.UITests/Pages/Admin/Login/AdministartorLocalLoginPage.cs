using IdentityServer4.Admin.UITests.Configuration;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Pages.Admin.Login
{
    internal class AdministartorLocalLoginPage : SeleniumPage
    {
        private string _loginRootUrl;// = "https://localhost:5000/idsrv/account/login";

        private IWebElement UsernameInput => Driver.FindElement(By.XPath(@"//*[@id='Username']"));
        private IWebElement PasswordInput => Driver.FindElement(By.XPath("//*[@id='Password']"));
        private IWebElement SubmitButton => Driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div[1]/div/div[2]/form/fieldset/div[4]/button[1]"));



        internal AdministartorLocalLoginPage(IWebDriver driver) : base(driver)
        {
            _loginRootUrl = $"{StsUrl.StsRootUrl}/idsrv/account/login";
        }

        public void DoLogin()
        {
            var user = TestUsers.LocalTestUser;
            if (Driver.Url.ToLower()
                .StartsWith(_loginRootUrl.ToLower()))
            {
                Username = user.Login;// "alice";
                Password = user.Password; // "alice";
                ClickSubmit();
            }
        }

        private string Username { set => UsernameInput.SendKeys(value); }

        private string Password { set => PasswordInput.SendKeys(value); }

        private void ClickSubmit() { SubmitButton.Click(); }
    }
}
