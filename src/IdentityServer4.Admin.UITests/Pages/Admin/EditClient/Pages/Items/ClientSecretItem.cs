using System;
using System.Diagnostics;
using IdentityServer4.Admin.UITests.Html.Elements;
using OpenQA.Selenium;

namespace Pluto.Test.UI.Api.Admin.EditClient.Pages.Items
{

    internal class ClientSecretItem
    {
        [DebuggerStepThrough]
        internal ClientSecretItem(IWebDriver driver, int rowNumber)
        {
            _deleteBtnFunc = () => new HtmlElement(driver.FindElement(By.XPath(_pointerDelBtn(rowNumber, 1))));

            _typeFunc = () => new HtmlElement(driver.FindElement(By.XPath(_pointer(rowNumber, 2))));
            _valueFunc = () => new HtmlElement(driver.FindElement(By.XPath(_pointer(rowNumber, 3))));
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

