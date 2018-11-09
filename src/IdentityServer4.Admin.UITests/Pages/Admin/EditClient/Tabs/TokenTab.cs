using IdentityServer4.Admin.UITests.Html.Elements;
using IdentityServer4.Admin.UITests.Html.Elements.Bootstrap;
using IdentityServer4.Admin.UITests.Pages.Admin.Base;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Pages.Admin.EditClient.Tabs
{
    internal class TokenTab : TabBase
    {
        internal HtmlInput IdentityTokenLifetime => new HtmlInput(ByXPath("//*[@id='IdentityTokenLifetime']"));
        internal HtmlInput AccessTokenLifetime => new HtmlInput(ByXPath("//*[@id='AccessTokenLifetime']"));
        internal HtmlInput AuthorizationCodeLifetime => new HtmlInput(ByXPath("//*[@id='AuthorizationCodeLifetime']"));
        internal HtmlInput AbsoluteRefreshTokenLifetime => new HtmlInput(ByXPath("//*[@id='AbsoluteRefreshTokenLifetime']"));
        internal HtmlInput SlidingRefreshTokenLifetime => new HtmlInput(ByXPath("//*[@id='SlidingRefreshTokenLifetime']"));


        internal HtmlSelect AccessTokenType => new HtmlSelect(ByXPath("//*[@id='AccessTokenType']"));
        internal HtmlSelect RefreshTokenUsage => new HtmlSelect(ByXPath("//*[@id='RefreshTokenUsage']"));
        internal HtmlSelect RefreshTokenExpiration => new HtmlSelect(ByXPath("//*[@id='RefreshTokenExpiration']"));

        internal TagPicker AllowedCorsOrigins => new TagPicker(Driver, "//*[@id='nav-token']/div/div/div[9]/div/div/div/div/div/input");


        internal ToggleSwitch UpdateAccessTokenClaimsOnRefresh => new ToggleSwitch(Driver, "//*[@id='UpdateAccessTokenClaimsOnRefresh']");
        internal ToggleSwitch IncludeJwtId => new ToggleSwitch(Driver, "//*[@id='IncludeJwtId']");
        internal ToggleSwitch AlwaysSendClientClaims => new ToggleSwitch(Driver, "//*[@id='AlwaysSendClientClaims']");
        internal ToggleSwitch AlwaysIncludeUserClaimsInIdToken => new ToggleSwitch(Driver, "//*[@id='AlwaysIncludeUserClaimsInIdToken']");


        internal HtmlInput ClientClaimsPrefix => new HtmlInput(ByXPath("//*[@id='ClientClaimsPrefix']"));
        internal HtmlInput PairWiseSubjectSalt => new HtmlInput(ByXPath("//*[@id='PairWiseSubjectSalt']"));

        internal HtmlLink ManageClientClaims => new HtmlLink(ByXPath("//*[@id='nav-token']/div/div/div[16]/div/a"));

        public TokenTab(IWebDriver driver, string tabPath) : base(driver, tabPath)
        {
        }

        public TokenTab(IWebDriver driver) : base(driver, "//*[@id='nav-token-tab']")
        {
            Header.Click();
        }
    }
}