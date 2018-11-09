using System;
using System.Collections.Generic;
using IdentityServer4.Admin.UITests.Html.Elements;
using IdentityServer4.Admin.UITests.Html.Elements.Bootstrap;
using IdentityServer4.Admin.UITests.Html.Extensions;
using OpenQA.Selenium;
using Pluto.Test.UI.Api.Admin.EditClient.Delete;

namespace Pluto.Test.UI.Api.Admin.EditClient.Pages
{
    internal class ClientClaimsPage : SeleniumPage
    {
        public TagPicker ClaimType => new TagPicker(Driver, "//*[@id='client-claims-form']/div/div/div[2]/div/div[1]/div/div/div/div/div/input[1]");
        public HtmlInput ClaimValue => new HtmlInput(ByXPath("//*[@id='Value']"));

        //public HtmlElement BackToClient => new HtmlElement(ByXPath("//*[@id='client-claims-form']/div/div/a"));
        public HtmlElement BackToClient => new HtmlElement(ByXPath("//*[@id='client-claims-form']/div/div[1]/nav/ol/li[2]/a"));

        public HtmlElement AddClientClaim => new HtmlElement(ByXPath("//*[@id='client-claims-button']"));

        internal ClientClaimsPage(IWebDriver driver) : base(driver)
        {
        }

        public void DeleteItem(int rowNumber)
        {
            var existingItem = GetItem(rowNumber);
            existingItem.DeleteBtn.Click();
            ClientClaimDeletePage deletePage = new ClientClaimDeletePage(Driver);
            deletePage.DeleteClientClaim.Click();
        }

        public ClientClaimItem GetItem(int rowNumber)
        {
            var res = new ClientClaimItem(Driver, rowNumber);
            res.Do(() => res._typeFunc.Invoke()).Until(() => res._typeFunc.Invoke() != null);
            return res;
        }

        public List<ClientClaimItem> GetItems()
        {
            List<ClientClaimItem> res = new List<ClientClaimItem>();
            for (int i = 0; i < 200; i++)
            {
                var item = new ClientClaimItem(Driver, i + 1);
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


        internal class ClientClaimItem
        {
            internal ClientClaimItem(IWebDriver driver, int rowNumber)
            {
                _typeFunc = () => new HtmlElement(driver.FindElement(By.XPath(_pointer(rowNumber, 1))));
                _valueFunc = () => new HtmlElement(driver.FindElement(By.XPath(_pointer(rowNumber, 2))));

                _deleteBtnFunc = () => new HtmlElement(driver.FindElement(By.XPath(_pointerDelBtn(rowNumber, 3))));
            }


            private string _pointer(int row, int col)
            {
                return $"/html/body/div[2]/div/div/div/div/div[1]/table/tbody/tr[{row}]/td[{col}]";
            }

            private string _pointerDelBtn(int row, int col)
            {
                return $"{_pointer(row, col)}/a";
            }

            private readonly Func<HtmlElement> _deleteBtnFunc;
            public Func<HtmlElement> _typeFunc;
            private readonly Func<HtmlElement> _valueFunc;


            public HtmlElement DeleteBtn => _deleteBtnFunc.Invoke();
            public HtmlElement Type => _typeFunc.Invoke();
            public HtmlElement Value => _valueFunc.Invoke();


        }

    }
}
