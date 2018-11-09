using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;

namespace Pluto.Test.UI.Api.TestCoreMvc
{
    [DebuggerStepThrough]
    internal class PostResultPage : SeleniumPage
    {
        internal PostResultPage(IWebDriver driver) : base(driver)
        {
        }

        // raw data
        public string RawResponse => Driver.FindElement(By.Id("raw")).Text;
        public string IdToken => Driver.FindElement(By.Id("id_token")).Text;
        public string AccessToken => Driver.FindElement(By.Id("access_token")).Text;


        // payload
        public string IdTokenClaims => Driver.FindElement(By.Id("id_token_claims")).Text;
        public string AccessTokenClaims => Driver.FindElement(By.Id("access_token_claims")).Text;
        public string AuthCode => Driver.FindElement(By.Id("code")).Text;
        public string UserInfoClaims => Driver.FindElement(By.Id("userinfo")).Text;

        public Claim[] GetAccessTokenClaims(params string[] excludeKeys)
        {
            return JObject.Parse(AccessTokenClaims).ToClaims(excludeKeys).ToArray();
        }
        public Claim[] GetIdTokenClaims(params string[] excludeKeys)
        {
            return JObject.Parse(IdTokenClaims).ToClaims(excludeKeys).ToArray();
        }

    }
}
