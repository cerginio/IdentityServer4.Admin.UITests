using System;
using System.Collections.Generic;
using IdentityServer4.Admin.UITests.Html.Elements;
using IdentityServer4.Admin.UITests.Html.Elements.Bootstrap;
using IdentityServer4.Admin.UITests.Html.Extensions;
using OpenQA.Selenium;

namespace Pluto.Test.UI.Api.Admin.EditResource.Pages
{
    internal class ApiScopesPage : SeleniumPage
    {
        internal ApiScopesPage(IWebDriver driver) : base(driver)
        {
        }
        internal HtmlInput Name => new HtmlInput(ByXPath("//*[@id='Name']"));

        internal HtmlInput DisplayName => new HtmlInput(ByXPath("//*[@id='DisplayName']"));

        internal HtmlInput Description => new HtmlInput(ByXPath("//*[@id='Description']"));

        internal ToggleSwitch Required => new ToggleSwitch(Driver, "//*[@id='Required']");

        internal ToggleSwitch Emphasize => new ToggleSwitch(Driver, "//*[@id='Emphasize']");

        internal TagPicker UserClaims => new TagPicker(Driver, "//*[@id='api-scope-form']/div[2]/div/div[6]/div/div/div/div/div/input[1]");


        public HtmlElement BackToResource => new HtmlElement(ByXPath("//*[@id='api-scope-form']/a"));

        public HtmlElement SaveApiScope => new HtmlElement(ByXPath("//*[@id='api-scope-save-button']"));




        public ApiScopeItem GetItem(int rowNumber)
        {
            var res = new ApiScopeItem(Driver, rowNumber);
            res.Do(() => res._nameFunc.Invoke()).Until(() => res._nameFunc.Invoke() != null);
            return res;
        }

        public List<ApiScopeItem> GetItems()
        {
            List<ApiScopeItem> res = new List<ApiScopeItem>();
            for (int i = 0; i < 200; i++)
            {
                var item = new ApiScopeItem(Driver, i + 1);
                try
                {
                    res.Do(() => item._nameFunc.Invoke()).Until(() => item._nameFunc.Invoke() != null);
                    res.Add(item);
                }
                catch
                {
                    break;
                }
            }

            return res;
        }


        // todo: List <Name, Edit, Delete>

        internal class ApiScopeItem
        {
            internal ApiScopeItem(IWebDriver driver, int rowNumber)
            {
                _nameFunc = () => new HtmlElement(driver.FindElement(By.XPath(_pointer(rowNumber, 1))));

                _editBtnFunc = () => new HtmlElement(driver.FindElement(By.XPath(_pointerBtn(rowNumber, 1))));
                _deleteBtnFunc = () => new HtmlElement(driver.FindElement(By.XPath(_pointerBtn(rowNumber, 2))));
            }


            private string _pointer(int row, int col)
            {
                return $"/html/body/div[2]/div/div/div/div/div[1]/table/tbody/tr[{row}]/td[{col}]";
            }

            private string _pointerBtn(int row, int col)
            {
                return $"/html/body/div[2]/div/div/div/div/div[1]/table/tbody/tr[{row}]/td[2]/a[{col}]";
            }

            public Func<HtmlElement> _nameFunc;

            private Func<HtmlElement> _editBtnFunc;
            private Func<HtmlElement> _deleteBtnFunc;


            public HtmlElement Name => _nameFunc.Invoke();

            public HtmlElement EditBtn => _editBtnFunc.Invoke();

            public HtmlElement DeleteBtn => _deleteBtnFunc.Invoke();



        }
    }

}
