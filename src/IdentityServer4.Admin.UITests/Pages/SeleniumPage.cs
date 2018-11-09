using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Pluto.Test.UI.Api
{
    internal class SeleniumPage
    {
        protected readonly IWebDriver Driver;
        protected string CurrentUrl { get; }

        protected SeleniumPage(IWebDriver driver)
        {
            Driver = driver;
            CurrentUrl = driver.Url;
        }

        public void Navigate()
        {
            Driver.Navigate().GoToUrl(CurrentUrl);
        }

        public bool IsOnPage(string expectedUri)
        {
            var uri = new Uri(Driver.Url);
            return expectedUri.TrimEnd(new[] { '/' })
                .EndsWith(uri.AbsolutePath.TrimEnd(new []{'/'}));
        }

        #region By syntax sugar around Driver

        public IWebElement ByXPath(string byXPath)
        {
            return Driver.FindElement(By.XPath(byXPath));
        }

        public IWebElement BySelector(string byCss)
        {
            return Driver.FindElement(By.CssSelector(byCss));
        }

        public IWebElement ById(string byId)
        {
            return Driver.FindElement(By.Id(byId));
        }

        #endregion By


        public bool IsElementExists(By by)
        {
            try
            {
                Driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        public void WaitForAjax()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
            wait.Until(d => (bool)(d as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));
        }

        protected void Fail(string message, Exception exception = null)
        {
            var sb = new StringBuilder()
                .AppendLine(message)
                .Append("Url: ")
                .AppendLine(Driver.Url);

            if (exception != null)
            {
                sb.AppendLine(exception.GetType().Name);
                sb.AppendLine(exception.Message);
            }

            Assert.Fail(sb.ToString());
        }
    }
}
