using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Html.Elements
{
    /// <summary>
    ///     Models HTML select, input or text area elements
    /// </summary>
    public abstract class HtmlControl : HtmlElement
    {
        /// <summary>
        ///     Initializes new instance of HTML element by calling base class constructor
        /// </summary>
        /// <param name="webElement">
        ///     WebElement wrapping WebDriver instance
        /// </param>
        protected HtmlControl(IWebElement webElement) : base(webElement)
        {
        }

        /// <summary>
        ///     Value assigned to control
        /// </summary>
        public string Value
        {
            get { return GetAttribute("value"); }
            //set { this.SetAttribute("value", value); }
        }

        /// <summary>
        ///     Disabled/enabled control state
        /// </summary>
        public bool Disabled
        {
            get { return _wrappedElement.Displayed; }
            
        }
    }
}