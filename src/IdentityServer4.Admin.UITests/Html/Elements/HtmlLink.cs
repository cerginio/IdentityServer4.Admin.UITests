using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Html.Elements {

    /// <summary>
    ///     Models HTML link element and exposes it's attributes as properties
    /// </summary>
    public class HtmlLink : HtmlElement
    {
        ///<summary>
        ///     Initializes new instance of HTML element by calling base class constructor
        /// </summary>
        /// <param name="webElement">
        ///     WebElement wrapping WebDriver instance
        /// </param>
        public HtmlLink(IWebElement webElement) : base(webElement)
        {
        }


        /// <summary>
        ///     Gets or sets 'href' attribute of the underlying link element or null if it does not exist
        /// </summary>
        public string Href {
            get { return GetAttribute("href"); }
            //set { this.SetAttribute("href", value); }
        }

        /// <summary>
        ///     Gets or sets 'target' attribute of the underlying link element or null if it does not exist
        /// </summary>
        public string Target {
            get { return GetAttribute("target"); }
            //set { this.SetAttribute("target", value); }
        }
    }

}