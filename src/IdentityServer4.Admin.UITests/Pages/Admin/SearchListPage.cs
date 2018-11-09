using IdentityServer4.Admin.UITests.Html.Elements;
using OpenQA.Selenium;

namespace Pluto.Test.UI.Api.Admin
{
    internal class SearchListPageBase : SeleniumPage
    {
        private HtmlInput SearchInput => new HtmlInput(ByXPath("/html/body/div[2]/div[1]/div[3]/form/div[1]/div/input"));

        private HtmlElement SearchSubmitBtn => new HtmlInput(ByXPath("/html/body/div[2]/div[1]/div[3]/form/div[2]/div/input"));

        public HtmlElement CreateNewItemBtn => new HtmlInput(ByXPath("/html/body/div[2]/div[1]/div[2]/a"));


        internal bool OpenItemForEditIfExists(string clientName)
        {
            SearchInput.EnterText(clientName);
            SearchSubmitBtn.Click();
            for (int i = 1; i < 5; i++)
            {

                if (!IsElementExists(By.XPath($"/html/body/div[2]/div[2]/div/div/table/tbody/tr[{i}]/td[1]")))
                {
                    continue;
                }

                var name = GetClientName(i);
                if (name.Text == clientName)
                {
                    EditClient(i).Click();
                    return true;
                }
            }

            return false;
        }


        private HtmlElement EditClient(int idx)
        {
            return new HtmlInput(ByXPath($"/html/body/div[2]/div[2]/div/div/table/tbody/tr[{idx}]/th/a"));
        }

        private HtmlElement GetClientName(int idx)
        {
            return new HtmlInput(ByXPath($"/html/body/div[2]/div[2]/div/div/table/tbody/tr[{idx}]/td[1]"));
        }

        internal SearchListPageBase(IWebDriver driver) : base(driver)
        {
        }
    }
}