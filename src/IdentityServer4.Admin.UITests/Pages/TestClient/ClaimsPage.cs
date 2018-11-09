using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Pluto.Test.UI.Api.TestCoreMvc
{
    internal class ClaimsPage : SeleniumPage
    {
        private IWebElement ClaimsTable => Driver.FindElement(By.TagName("table"));

        public ClaimsPage(IWebDriver driver) : base(driver)
        {
        }

        public IEnumerable<Tuple<string,string>> Claims
        {
            get
            {
                foreach (var row in ClaimsTable.FindElements(By.TagName("tr")))
                {
                    var name = row.FindElement(By.TagName("th")).Text;
                    var value = row.FindElement(By.TagName("td")).Text;
                    yield return new Tuple<string, string>(name, value);
                }
            }
        }
    }
}
