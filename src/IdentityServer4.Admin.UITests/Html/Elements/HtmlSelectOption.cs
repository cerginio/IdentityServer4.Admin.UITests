using IdentityServer4.Admin.UITests.Html.Extensions;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Html.Elements {

    /// <summary>
    ///     Models options within HTML select element
    /// </summary>
    public class HtmlSelectOption : HtmlControl {

        /// <summary>
        ///     Initializes new instance of HTML element by calling base class constructor
        /// </summary>
        /// <param name="webElement">
        ///     WebElement wrapping WebDriver instance
        /// </param>
        public HtmlSelectOption(IWebElement webElement) : base(webElement) {}

        /// <summary>
        ///     Mark option as selected.
        /// </summary>
        public void SelectOption()
        {
            this.Do(Click).Until(() => Selected);
        }

        /// <summary>
        ///     Remove selection if it was present.
        /// </summary>
        public void DelesectOption()
        {
            this.Do(Click).Until(() => !Selected);
        }
    }

}