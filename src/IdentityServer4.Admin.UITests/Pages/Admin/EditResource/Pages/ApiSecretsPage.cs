using IdentityServer4.Admin.UITests.Html.Elements;
using OpenQA.Selenium;
using Pluto.Test.UI.Api.Admin.EditClient.Pages;

namespace Pluto.Test.UI.Api.Admin.EditResource.Pages
{
    internal class ApiSecretsPage : SecretsPageBase
    {
        public HtmlElement BackToResource => new HtmlElement(ByXPath("/html/body/div[2]/form/div/div[1]/nav/ol/li[2]/a"));


        public override HtmlElement AddSecret => new HtmlElement(ByXPath("/html/body/div[2]/form/div/div[4]/div/div/div[4]/div/button"));
        //                                                                

        public ApiSecretsPage(IWebDriver driver) : base(driver)
        {
        }

    }
}