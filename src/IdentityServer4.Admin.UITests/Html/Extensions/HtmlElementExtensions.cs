using IdentityServer4.Admin.UITests.Html.Elements;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Html.Extensions
{
    public static class HtmlElementExtensions
    {

        public static T Get<T>(this HtmlElement element, By by) where T : class, IWebElement
        {
            return element.FindElement(by) as T;
        }

       
    }
}
