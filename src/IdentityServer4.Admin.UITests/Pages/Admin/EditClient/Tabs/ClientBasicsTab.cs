using IdentityServer4.Admin.UITests.Html.Elements;
using IdentityServer4.Admin.UITests.Html.Elements.Bootstrap;
using OpenQA.Selenium;
using Pluto.Test.UI.Api.Admin.Base;

namespace Pluto.Test.UI.Api.Admin.EditClient.Tabs
{
    internal class ClientBasicsTab : TabBase
    {

        internal HtmlInput Description => new HtmlInput(ByXPath("//*[@id='Description']"));

        internal HtmlSelect ProtocolType => new HtmlSelect(ByXPath("//*[@id='ProtocolType']"));
        //internal ToggleSwitch RequireClientSecret2 =>
        //    new ToggleSwitch(
        //        ByXPath("//*[@id='nav-basics']/div/div/div[4]/div/label/div"),
        //        new HtmlInput(ByXPath("//*[@id='RequireClientSecret']")));

        internal ToggleSwitch RequireClientSecret => new ToggleSwitch(Driver, "//*[@id='RequireClientSecret']");
        internal ToggleSwitch RequirePkce => new ToggleSwitch(Driver, "//*[@id='RequirePkce']");
        internal ToggleSwitch AllowPlainTextPkce => new ToggleSwitch(Driver, "//*[@id='AllowPlainTextPkce']");
        internal ToggleSwitch AllowOfflineAccess => new ToggleSwitch(Driver, "//*[@id='AllowOfflineAccess']");
        internal ToggleSwitch AllowAccessTokensViaBrowser => new ToggleSwitch(Driver, "//*[@id='AllowAccessTokensViaBrowser']");


        internal TagPicker AllowedScopes => new TagPicker(Driver, "//*[@id='nav-basics']/div/div/div[9]/div/div/div/div/div/input[1]");
        internal TagPicker RedirectUris => new TagPicker(Driver, "//*[@id='nav-basics']/div/div/div[10]/div/div/div/div/div/input[1]");
        internal TagPicker AllowedGrantTypes => new TagPicker(Driver, "//*[@id='nav-basics']/div/div/div[11]/div/div/div/div/div/input[1]");

        internal HtmlLink ManageSecrets => new HtmlLink(ByXPath("//*[@id='nav-basics']/div/div/div[12]/div/a"));
        internal HtmlLink ManageClientProperties => new HtmlLink(ByXPath("//*[@id='nav-basics']/div/div/div[13]/div/a"));

        public ClientBasicsTab(IWebDriver driver) : base(driver, "//*[@id='nav-basics-tab']")
        {
            Header.Click();
        }

        /* Edit client
        tabs
        basics
         //*[@id="nav-basics-tab"]
         //*[@id="Description"]
        selector
         //*[@id="ProtocolType"]
         toggle
         //*[@id="nav-basics"]/div/div/div[4]/div/label/div\         
         //*[@id="nav-basics"]/div/div/div[4]/div/label
         //*[@id="RequireClientSecret"]

         //*[@id="RequireClientSecret"]
        allowed scopes items
         //*[@id="nav-basics"]/div/div/div[9]/div/div/div/div/div/div[5]/button[1]/span[1]
         //*[@id="nav-basics"]/div/div/div[9]/div/div/div/div/div/div[5]/button[2]/span[1]

         allowed grant types items
         //*[@id="nav-basics"]/div/div/div[11]/div/div/div/div/div/div[5]/button/span[1]
         //*[@id="nav-basics"]/div/div/div[11]/div/div/div/div/div/div[5]/button[1]/span[1]
         //*[@id="nav-basics"]/div/div/div[11]/div/div/div/div/div/div[5]/button[2]/span[1]
         manage secrets
        //*[@id="nav-basics"]/div/div/div[12]/div/a
        */

    }
}