using System.Linq;
using System.Security.Claims;
using System.Text;
using IdentityModel;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Pages.TestClient
{
    internal class FragmentResultPage : SeleniumPage
    {
        internal FragmentResultPage(IWebDriver driver) : base(driver)
        {
        }

        // raw data
        public string RawResponse => Driver.FindElement(By.Id("raw")).Text;
        public string IdToken => Driver.FindElement(By.Id("id_token")).Text;

        // payload
      
        public Claim[] GetIdTokenClaims(params string[] excludeKeys)
        {

            var parts = IdToken.Split('.');
            //var headerPart = parts[0];
            var claimsPart = parts[1];

            // Decode Base64
            //var jsonHeader = JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(headerPart)));
            //var jsonClaims = JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(claimsPart)));
            return JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(claimsPart)))
                .ToClaims(excludeKeys).ToArray();
        }

    }
}