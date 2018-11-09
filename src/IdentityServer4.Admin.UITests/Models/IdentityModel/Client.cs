using System;
using System.Collections.Generic;

namespace Pluto.Test.UI.Steps.Administration.Models.IdentityModel
{
    public class Client
    {
        public int Id { get; set; }
        public bool Enabled { get; set; } = true;
        public string ClientId { get; set; }
        public string ProtocolType { get; set; } = IdentityServerConstants.ProtocolTypes.OpenIdConnect;
        public List<ClientSecret> ClientSecrets { get; set; }
        public bool RequireClientSecret { get; set; } = true;
        public string ClientName { get; set; }
        public string Description { get; set; }
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }
        public bool RequireConsent { get; set; } = true;
        public bool AllowRememberConsent { get; set; } = true;
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
        public List<string> AllowedGrantTypes { get; set; }
        public bool RequirePkce { get; set; }
        public bool AllowPlainTextPkce { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public List<string> RedirectUris { get; set; }
        public List<string> PostLogoutRedirectUris { get; set; }
        public string FrontChannelLogoutUri { get; set; }
        public bool FrontChannelLogoutSessionRequired { get; set; } = true;
        public string BackChannelLogoutUri { get; set; }
        public bool BackChannelLogoutSessionRequired { get; set; } = true;
        public bool AllowOfflineAccess { get; set; }
        public List<string> AllowedScopes { get; set; }
        public int IdentityTokenLifetime { get; set; } = 300;
        public int AccessTokenLifetime { get; set; } = 3600;
        public int AuthorizationCodeLifetime { get; set; } = 300;
        public int? ConsentLifetime { get; set; } = null;
        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;
        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;
        public int RefreshTokenUsage { get; set; }// = (int)TokenUsage.OneTimeOnly;
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public int RefreshTokenExpiration { get; set; }// = (int)TokenExpiration.Absolute;
        public int AccessTokenType { get; set; } = (int)0; // AccessTokenType.Jwt;
        public bool EnableLocalLogin { get; set; } = true;
        public List<string> IdentityProviderRestrictions { get; set; }
        public bool IncludeJwtId { get; set; }
        public List<ClientClaim> Claims { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public string ClientClaimsPrefix { get; set; } = "client_";
        public string PairWiseSubjectSalt { get; set; }
        public List<string> AllowedCorsOrigins { get; set; }
        public List<ClientProperty> Properties { get; set; }
    }

    public class ClientProperty
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    //public class ClientGrantType
    //{
    //    public int Id { get; set; }
    //    public string GrantType { get; set; }
    //}

    public class ClientSecret : Secret
    {
        public ClientSecret(string value)
        {
            Value = value;
        }
    }

    public abstract class Secret
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
        public string Type { get; set; } = "SharedSecret"; // = SecretTypes.SharedSecret;
    }

    //public class ClientRedirectUri
    //{
    //    public int Id { get; set; }
    //    public string RedirectUri { get; set; }
    //}

    //public class ClientPostLogoutRedirectUri
    //{
    //    public int Id { get; set; }
    //    public string PostLogoutRedirectUri { get; set; }
    //}

    //public class ClientScope
    //{
    //    public int Id { get; set; }
    //    public string Scope { get; set; }
    //}

    //public class ClientCorsOrigin
    //{
    //    public int Id { get; set; }
    //    public string Origin { get; set; }
    //}


    public class ClientClaim
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    //public class ClientIdPRestriction
    //{
    //    public int Id { get; set; }
    //    public string Provider { get; set; }
    //}
}


