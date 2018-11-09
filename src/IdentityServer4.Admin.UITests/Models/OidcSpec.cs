using IdentityModel;

namespace Pluto.Test.UI
{
    public class OidcSpec
    {
        public string clientId { get; set; } = "testMvc";
        public string responseType { get; set; } = OidcConstants.ResponseTypes.IdTokenToken;
        public string scope { get; set; } = "openid profile idm roles";
        public string redirectUri { get; set; }
        public string acrValues { get; set; } = "idp:ifaccounts";
        public string responseMode { get; set; } = OidcConstants.ResponseModes.FormPost;
    }
}