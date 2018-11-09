using IdentityModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pluto.Test.UI.Api.TestCoreMvc;
using Pluto.Test.UI.IdentityModel216;

namespace Pluto.Test.UI
{
    // TODO: SolutionUrl rename
    // OpenIdStsRootUrl, AdminRootUrl => STSRootUrl, AdminRootUrl
    internal static class StsUrl
    {
        public static string AuthorizeEndpoint => StsRootUrl + "/idsrv/connect/authorize";
        public static string WellKnownConfigEndpoint => StsRootUrl + "/idsrv/.well-known/openid-configuration";

        public static string StsRootUrl => Settings.Get("OpenIdStsRootUrl");

        public static string AdminRootUrl => Settings.Get("AdminRootUrl");

        public static string GetAdminRoot()
        {
            var adminRoot = AdminRootUrl;
            if (string.IsNullOrEmpty(adminRoot))
            {
                Assert.Inconclusive();
            }
            return adminRoot;
        }

        public static string Authorize(string acrValues)
        {
            return new AuthorizeRequest(AuthorizeEndpoint)
                .CreateAuthorizeUrl(
                    clientId: "testMvc",
                    responseType: OidcConstants.ResponseTypes.IdToken,
                    scope: "openid",
                    redirectUri: TestClientUrl.RedirectUri,
                    nonce: "foo",
                    acrValues: acrValues,
                    responseMode: OidcConstants.ResponseModes.FormPost
                );
        }

        /// <summary>
        /// Authorize With Code Challenge
        /// allows for hybrid client setup to get in one request: auth_code, id_token and access_token 
        /// </summary>
        /// <param name="oidc"></param>
        /// <returns>
        /// Item1: AuthorizeUrl,
        /// Item2: challenge,
        /// Item3: verifier
        /// </returns>
        public static (string,string,string) AuthorizeUrlWithCodeChallenge(OidcSpec oidc)
        {
            // https://leastprivilege.com/2016/02/02/pkce-support-in-identityserver-and-identitymodel/

            var nonce = CryptoRandom.CreateRandomKeyString(64);
            var verifier = CryptoRandom.CreateRandomKeyString(64);
            var challenge = verifier.ToSha256();

            return (new AuthorizeRequest(AuthorizeEndpoint)
                .CreateAuthorizeUrl(
                    clientId: oidc.clientId,
                    responseType: oidc.responseType,// OidcConstants.ResponseTypes.CodeIdTokenToken,
                    scope: oidc.scope,//"openid profile idm roles",
                    redirectUri: oidc.redirectUri,// $"http://localhost:1391/Home/PostResult",
                    nonce: nonce,
                    acrValues: oidc.acrValues,
                    responseMode: oidc.responseMode,//OidcConstants.ResponseModes.FormPost
                    codeChallenge: challenge,
                    codeChallengeMethod: OidcConstants.CodeChallengeMethods.Sha256

                ), challenge, verifier);
        }

        /// <summary>
        /// Implicit
        /// </summary>
        /// <param name="oidc"></param>
        /// <returns></returns>
        public static string AuthorizeUrl(OidcSpec oidc)
        {
            // https://leastprivilege.com/2016/02/02/pkce-support-in-identityserver-and-identitymodel/
            var nonce = CryptoRandom.CreateRandomKeyString(64);


            return new AuthorizeRequest(AuthorizeEndpoint)
                .CreateAuthorizeUrl(
                    clientId: oidc.clientId,
                    responseType: oidc.responseType,// OidcConstants.ResponseTypes.IdTokenToken,
                    scope: oidc.scope,//"openid profile idm roles",
                    redirectUri: oidc.redirectUri,// $"http://localhost:1391/Home/PostResult",
                    responseMode: oidc.responseMode,//OidcConstants.ResponseModes.FormPost
                    nonce: nonce,
                    acrValues: oidc.acrValues
                );
        }

    }
}