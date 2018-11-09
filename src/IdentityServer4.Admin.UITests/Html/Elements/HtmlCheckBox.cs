using IdentityServer4.Admin.UITests.Html.Extensions;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Html.Elements
{
    /// <summary>
    ///     Models HTML input with checkbox or radio button type
    /// </summary>
    public class HtmlCheckBox : HtmlInput
    {
        ///<summary>
        ///     Initializes new instance of HTML element by calling base class constructors
        /// </summary>
        /// <param name="webElement">
        ///     WebElement wrapping WebDriver instance
        /// </param>
        public HtmlCheckBox(IWebElement webElement) : base(webElement)
        {
        }

        /// <summary>
        ///     Specifies whether checkbox or radio button is selected and allows to change it
        /// </summary>
        public virtual bool Checked
        {
            get { return Selected; }
            set { this.Do(Click).Until(self => Selected == value); }
        }

        /// <summary>
        ///     Marks checkbox as selected.
        /// </summary>
        public void Select()
        {
            Checked = true;
        }
    }
}