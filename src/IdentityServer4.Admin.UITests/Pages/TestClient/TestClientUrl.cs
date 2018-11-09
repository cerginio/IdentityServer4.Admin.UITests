using System.Collections.Generic;

namespace Pluto.Test.UI.Api.TestCoreMvc
{
    internal class TestClientUrl
    {
        public static string RootUrl => Settings.Get("TestMvcClientRootUrl");

        //public static string ClaimsUrl => RootUrl + "/Home/Secured";

        public static string RedirectUri => RootUrl + "/signin-oidc";

        public static string PostResultUri => RootUrl + "/Home/PostResult";
        public static string FragmentResultUri => RootUrl + "/Home/FragmentResult";

        //public static string PostLogoutUri => RootUrl + "$/Account/SignedOut";

        public static List<string> PostLogoutUris => new List<string>
        {
            RootUrl + "/Account/SignedOut",
            RootUrl + "/signout-callback-oidc",
        };

        //public static string SignIn(string idp, string classRef = null)
        //{
        //    return RootUrl + $"/Account/Signin?idp={idp}&classRef={classRef}";
        //}

        ///// 
        //public static string DynamicSignIn(string idp, string clientId = null)
        //{
        //    return RootUrl + $"/Account/DynamicSignIn?idp={idp}&overridedClientid={clientId}";
        //}
    }
}
