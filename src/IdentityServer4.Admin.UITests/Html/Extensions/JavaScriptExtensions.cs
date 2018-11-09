using System;
using IdentityServer4.Admin.UITests.Html.Elements;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Html.Extensions
{
    // https://github.com/eger-geger/Selenium.HtmlElements.Net/blob/master/Selenium.HtmlElements/src/Extensions/JavaScriptExtensions.cs
    //
    /// <summary>
    ///     Provides methods of manipulating DOM element via JavaScript
    /// </summary>
    public static class JavaScriptExtensions
    {

        /// <summary>
        ///     Execute JavaScript code in browser replacing all occurrences of <c>{self}</c> with actual DOM element being pointed to by current web element.
        /// </summary>
        /// <param name="element">Target element</param>
        /// <param name="jsSnippet">JavaScript snippet to execute in browser</param>
        /// <param name="arguments">Arguments passed to JavaScript snippet</param>
        /// <returns>Result of the script execution</returns>
        public static object ExecuteScriptOnSelf(this IHtmlElement element, IWebDriver driver, String jsSnippet, params Object[] arguments)
        {
            var extendedArguments = new Object[arguments.Length + 1];
            extendedArguments[arguments.Length] = element.WrappedElement;
            arguments.CopyTo(extendedArguments, 0);

            return ((IJavaScriptExecutor)driver).ExecuteScript(
                jsSnippet.Replace("{self}", String.Format("arguments[{0}]", arguments.Length)), extendedArguments
            );
        }

        /// <summary>
        ///     Execute JavaScript code in browser replacing all occurrences of <c>{self}</c> with actual 
        ///     DOM element being pointed to by current web element and convert result to specific type.
        /// </summary>
        /// <param name="element">Target element</param>
        /// <param name="jsSnippet">JavaScript snippet to execute in browser</param>
        /// <param name="arguments">Arguments passed to JavaScript snippet</param>
        /// <returns>Result of the script execution or default value for a given type</returns>
        public static TReturn ExecuteScriptOnSelf<TReturn>(this IHtmlElement element, IWebDriver driver, String jsSnippet, params Object[] arguments)
        {
            var result = element.ExecuteScriptOnSelf(driver, jsSnippet, arguments);

            if (result is TReturn)
            {
                return (TReturn)result;
            }

            return default(TReturn);
        }

        /// <summary>
        ///     Checks attribute exists in DOM element
        /// </summary>
        /// <param name="element">Target element</param>
        /// <param name="attributeName">Attribute name</param>
        /// <returns>
        ///     <c>true</c> if attribute exists and <c>false</c> otherwise
        /// </returns>
        public static bool HasAttribute(this IHtmlElement element, IWebDriver driver, string attributeName)
        {
            return element.ExecuteScriptOnSelf<Boolean?>(driver,
                "return {self}.hasAttribute(arguments[0]);", attributeName
            ).GetValueOrDefault(false);
        }

        /// <summary>
        ///     Set DOM element attribute value
        /// </summary>
        /// <param name="element">Target element</param>
        /// <param name="attributeName">Attribute name</param>
        /// <param name="attributeValue">Attribute value</param>
        public static void SetAttribute(this IHtmlElement element, IWebDriver driver, String attributeName, String attributeValue)
        {
            element.ExecuteScriptOnSelf(driver, "{self}.setAttribute(arguments[0], arguments[1]);", attributeName, attributeValue);
        }

        /// <summary>
        ///     Remove DOM element attribute
        /// </summary>
        /// <param name="element">Target element</param>
        /// <param name="attributeName">Attribute name</param>
        public static void RemoveAttribute(this IHtmlElement element, IWebDriver driver, String attributeName)
        {
            element.ExecuteScriptOnSelf(driver, "{self}.removeAttribute(arguments[0]);", attributeName);
        }

        /// <summary>
        ///     Checks weather property exist in given DOM element object
        /// </summary>
        /// <param name="element">Target element</param>
        /// <param name="propertyName">Property name</param>
        /// <returns>
        ///     <c>true</c> if property with a given name exist and <c>false</c> otherwise
        /// </returns>
        public static bool HasProperty(this IHtmlElement element, IWebDriver driver, String propertyName)
        {
            return element.ExecuteScriptOnSelf<Boolean>(driver, "return !!{self}[arguments[0]];");
        }

        /// <summary>
        ///     Set property value for given DOM element object
        /// </summary>
        /// <param name="element"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        public static void SetPropery(this IHtmlElement element, IWebDriver driver, String propertyName, Object propertyValue)
        {
            element.ExecuteScriptOnSelf(driver, "{self}[arguments[0]] = arguments[1];", propertyName, propertyValue);
        }

        /// <summary>
        ///     Get value of a given DOM element property
        /// </summary>
        /// <param name="element">Target element</param>
        /// <param name="propertyName">Property name</param>
        /// <returns>Value of the property</returns>
        public static T GetProperty<T>(this IHtmlElement element, IWebDriver driver, String propertyName)
        {
            return (T)element.ExecuteScriptOnSelf(driver, "return {self}[arguments[0]];", propertyName);
        }

    }

}