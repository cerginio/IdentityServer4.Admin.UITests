using System.Collections.Generic;
using System.Diagnostics;
using IdentityServer4.Admin.UITests.Html.Elements;
using IdentityServer4.Admin.UITests.Html.Extensions;
using IdentityServer4.Admin.UITests.Pages.Admin.EditClient.Delete;
using IdentityServer4.Admin.UITests.Pages.Admin.EditClient.Pages.Items;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Pages.Admin.EditClient.Pages
{
    internal class SecretsPageBase : SeleniumPage
    {
        public HtmlSelect Type => new HtmlSelect(ByXPath("//*[@id='Type']"));
        public HtmlInput Value => new HtmlInput(ByXPath("//*[@id='Value']"));
        public HtmlSelect HashType => new HtmlSelect(ByXPath("//*[@id='HashType']"));
        public virtual HtmlElement AddSecret => new HtmlElement(ByXPath("/html/body/div[2]/form/div/div[4]/div/div/div[4]/div/button"));


        public void DeleteItem(int rowNumber)
        {
            var existingItem = GetItem(rowNumber);
            existingItem.DeleteBtn.Click();

            ClientSecretDeletePage deletePage = new ClientSecretDeletePage(Driver);
            deletePage.DeleteClientClaim.Click();
        }


        public ClientSecretItem GetItem(int rowNumber)
        {
            var res = new ClientSecretItem(Driver, rowNumber);

            res.Do(() => res._typeFunc.Invoke()).Until(() => res._typeFunc.Invoke() != null);
            return res;

        }

        [DebuggerStepThrough]
        public List<ClientSecretItem> GetItems()
        {
            List<ClientSecretItem> res = new List<ClientSecretItem>();
            for (int i = 0; i < 200; i++)
            {
                var item = new ClientSecretItem(Driver, i + 1);
                try
                {
                    res.Do(() => item._typeFunc.Invoke()).Until(() => item._typeFunc.Invoke() != null);
                    res.Add(item);
                }
                catch
                {
                    break;
                }
            }

            return res;
        }

        public SecretsPageBase(IWebDriver driver) : base(driver)
        {
        }

    }
}