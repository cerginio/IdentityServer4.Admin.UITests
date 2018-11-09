using System;
using System.IO;
using System.Net;
using IdentityServer4.Admin.UITests.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;

namespace IdentityServer4.Admin.UITests.Tests.Admin.Base
{
    public class SeleniumTest
    {
        protected IWebDriver Driver;
        protected SoftAssertions SoftAssert;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void SetupTestSuite()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            SoftAssert = new SoftAssertions();

            Settings.SetTestContext(TestContext);

            var driver = Settings.Get("SeleniumDriver");

            switch (driver?.ToLower())
            {
                case "chrome":
                    Driver = new ChromeDriver(Environment.CurrentDirectory);
                    break;

                case "chromeheadless":
                    var options = new ChromeOptions();
                    options.AddArgument("headless");
                    Driver = new ChromeDriver(Environment.CurrentDirectory, options);
                    break;

                default:
                    throw new Exception($"Unknown driver '{driver}' in app setting SeleniumDriver.");
            }
        }



        [TestCleanup]
        public void CleanupTestSuite()
        {
            if (Driver != null)
            {
                TestContext.WriteLine($"Final browser address: {Driver.Url}");

                if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
                {
                    TakeScreenshot("Error");
                }

                Driver.Quit();
            }

            SoftAssert.AssertAll();
        }

        public void TakeScreenshot(string name)
        {
            var fileName = Path.Combine(Environment.CurrentDirectory, $"{name}_screenshot.png");

            var screenshot = Driver.TakeScreenshot();
            screenshot.SaveAsFile(fileName);

            // https://github.com/Microsoft/testfx/issues/394
            // No 'AddResultFile' implementation in TestContext on .NET Core 2.0 test project #394
            //TestContext.AddResultFile(fileName);
        }

        public void RestartBrowser()
        {
            Driver.Quit();
            SetupTestSuite();
        }
    }
}
