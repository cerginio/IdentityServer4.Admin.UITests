using System;
using System.Collections.Generic;
using IdentityServer4.Admin.UITests.Html.Elements;
using IdentityServer4.Admin.UITests.Html.Extensions;
using IdentityServer4.Admin.UITests.Pages.Admin.EditClient.Delete;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Pages.Admin.EditClient.Pages
{
    internal class ClientPropertiesPage : SeleniumPage
    {
        public HtmlSelect Key => new HtmlSelect(ByXPath("//*[@id='Key']"));
        public HtmlInput Value => new HtmlInput(ByXPath("//*[@id='Value']"));

        public HtmlElement AddProperty => new HtmlElement(ByXPath("/html/body/div[2]/form/div/div/div[2]/div/div[3]/div/button"));

        public HtmlElement BackToClient => new HtmlElement(ByXPath("/html/body/div[2]/form/div/div[1]/nav/ol/li[2]/a"));
            
        public void DeleteItem(int rowNumber)
        {
            var existingItem = GetItem(rowNumber);
            existingItem.DeleteBtn.Click();

            ClientSecretDeletePage deletePage = new ClientSecretDeletePage(Driver);
            deletePage.DeleteClientClaim.Click();
        }
      

        public ClientPropertyItem GetItem(int rowNumber)
        {
            var res = new ClientPropertyItem(Driver, rowNumber);

            res.Do(() => res._keyFunc.Invoke()).Until(() => res._keyFunc.Invoke() != null);
            return res;
        }
        public List<ClientPropertyItem> GetItems()
        {
            List<ClientPropertyItem> res = new List<ClientPropertyItem>();
            for (int i = 0; i < 200; i++)
            {
                var item = new ClientPropertyItem(Driver, i + 1);
                try
                {
                    res.Do(() => item._keyFunc.Invoke()).Until(() => item._keyFunc.Invoke() != null);
                    res.Add(item);
                }
                catch
                {
                    break;
                }
            }

            return res;
        }

        public ClientPropertiesPage(IWebDriver driver) : base(driver)
        {
        }

        internal class ClientPropertyItem
        {
            internal ClientPropertyItem(IWebDriver driver, int rowNumber)
            {
                _keyFunc = () => new HtmlElement(driver.FindElement(By.XPath(_pointer(rowNumber, 1))));
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
            public Func<HtmlElement> _keyFunc;
            private readonly Func<HtmlElement> _valueFunc;


            public HtmlElement DeleteBtn => _deleteBtnFunc.Invoke();
            public HtmlElement Key => _keyFunc.Invoke();
            public HtmlElement Value => _valueFunc.Invoke();
        }

    }
}