using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Html.Elements
{
    /// <summary>
    ///     Models table DOM element and provides access to individual rows and columns.
    /// </summary>
    /// <seealso cref="HtmlElement" />
    public class HtmlTable : HtmlElement
    {

        ///<summary>
        ///     Initializes new instance of HTML element by calling base class constructor.
        /// </summary>
        /// <param name="wrapped">
        ///     WebElement wrapping WebDriver instance.
        /// </param>
        public HtmlTable(IWebElement wrapped) : base(wrapped)
        {
        }

        /// <summary>
        ///     Get list of cells in a column with given index.
        /// </summary>
        /// <param name="index">
        ///     Table column index.
        /// </param>
        /// <returns>
        ///     List of column cells.
        /// </returns>
        public IList<IWebElement> Column(int index)
        {
            return _wrappedElement.FindElements(By.CssSelector(String.Format("tr>*:nth-child({0})", index)));
        }

        /// <summary>
        ///     Get list of cells in a row with given index.
        /// </summary>
        /// <param name="index">
        ///     Table row index.
        /// </param>
        /// <returns>
        ///     List of row cells.
        /// </returns>
        public IList<IWebElement> Row(int index)
        {
            return _wrappedElement.FindElements(By.CssSelector(String.Format("tr:nth-child({0})>*", index)));
        }

    }
}
