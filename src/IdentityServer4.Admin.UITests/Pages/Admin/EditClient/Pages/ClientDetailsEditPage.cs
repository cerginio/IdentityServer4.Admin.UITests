using IdentityServer4.Admin.UITests.Html.Elements;
using IdentityServer4.Admin.UITests.Pages.Admin.EditClient.Tabs;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Pages.Admin.EditClient.Pages
{
    internal class ClientDetailsEditPage : SeleniumPage
    {
        public HtmlElement SaveClientBtn => new HtmlInput(ByXPath("//*[@id='client-form']/div[4]/div/button"));
        public HtmlElement CloneClientBtn => new HtmlInput(ByXPath("//*[@id='client-form']/div[4]/div/a[1]"));
        public HtmlElement DeleteClientBtn => new HtmlInput(ByXPath("//*[@id='client-form']/div[4]/div/a[2]"));

        public ClientNameTab NameTab => new ClientNameTab(Driver);
        public ClientBasicsTab BasicsTab => new ClientBasicsTab(Driver);
        public AuthenticationLogoutTab AuthenticationLogoutTab => new AuthenticationLogoutTab(Driver);
        public TokenTab TokenTab => new TokenTab(Driver);
        public ConsentScreenTab ConsentScreenTab => new ConsentScreenTab(Driver);

        //*[@id='nav-authentication-tab']
        //*[@id='nav-token-tab']
        //*[@id='nav-consent-tab']

        internal ClientDetailsEditPage(IWebDriver driver) : base(driver)
        {
        }
    }
}