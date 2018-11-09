using IdentityServer4.Admin.UITests.Html.Elements;
using OpenQA.Selenium;

namespace Pluto.Test.UI.Api.Admin.EditClient.Pages
{
    internal partial class ClientSecretsPage : SecretsPageBase
    {public HtmlElement BackToClient => new HtmlElement(ByXPath("/html/body/div[2]/form/div/div[1]/nav/ol/li[2]/a"));

        public override HtmlElement AddSecret => new HtmlElement(ByXPath("/html/body/div[2]/form/div/div/div[2]/div/div[4]/div/button"));


        public ClientSecretsPage(IWebDriver driver) : base(driver)
        {
        }

    }
}
