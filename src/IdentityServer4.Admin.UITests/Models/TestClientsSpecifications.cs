using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Admin.UITests.Models.IdentityModel;
using IdentityServer4.Admin.UITests.Models.Pluto;

namespace IdentityServer4.Admin.UITests.Models
{
    public class TestClientsSpecifications
    {

        public IEnumerable<IdentityResource> GetIdentityResourcesCopies()
        {
            return new List<IdentityResource>
            {
                new IdentityResourcesStub.OpenId(),
                new IdentityResourcesStub.Profile(),
                new IdentityResourcesStub.Email(),
                new MyProjectIdentityResourcesStub.Roles(),
            };
        }

        public IEnumerable<string> GetClientIds()
        {
            return new List<string>()
            {
            "testClient",
            "testJsImplicit",
            "testMvc",
            };
        }

        public IEnumerable<ApiResource> GetApiResourcesCopies()
        {
            return new List<ApiResource>
            {
                new ApiResource("testapi1", "My test API")
                {
                    DisplayName = "testapi1",
                    Description = "My test API",
                    UserClaims = new List<string>(){"testResClaim1","testResClaim2"},
                    Enabled = true,
                    Scopes = new List<Scope>(){new Scope()
                    {
                        Name = "testScope1",
                        Description = "testScope1",
                        DisplayName =  "test scope number one",
                        Emphasize = true,
                        Required = true,
                        ShowInDiscoveryDocument = true,
                        UserClaims = new List<string>(){ "testScopeClaim1", "testScopeClaim2" }
                    }},
                    ApiSecrets = new List<Secret>(){new ClientSecret("testApiSecret1")},
                }
            };
        }




        // Typical clients "copies" for Admin UI controls testing
        // - the most detailed settings without real reason
        // - minimal amount of default values
        public IEnumerable<Client> GetClientCopies()
        {
            // client credentials client
            return new List<Client>
            {
                // For flows integration test
                new Client
                {
                    ClientId = "testClient",
                    ClientName = "Test Client",
                    ClientSecrets = new List<ClientSecret>()
                    {
                        new ClientSecret("secret")
                    },
                    AllowedGrantTypes = new List<string>()
                    {
                        OidcConstants.GrantTypes.Implicit,
                        OidcConstants.GrantTypes.ClientCredentials,
                        //OidcConstants.GrantTypes.AuthorizationCode,
                        OidcConstants.GrantTypes.Password,
                    },
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = new List<string>()
                    {
                        "https://localhost:44390/",
                        "https://localhost:44390/index.html",
                        "https://localhost:44390/popup.html",
                        "https://localhost:44390/renew.html",
                    },
                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        MyProjectIdsConst.MyProjectScopes.Roles,
                        "testApi"
                    },
                    AllowedCorsOrigins =  new List<string>() {"https://localhost:44390"}
                },

                new Client
                {
                    ClientId = "testMvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = new List<string>()
                    {
                        OidcConstants.GrantTypes.Implicit,
                        OidcConstants.GrantTypes.AuthorizationCode,
                        OidcConstants.GrantTypes.ClientCredentials,
                        OidcConstants.GrantTypes.Password,
                    },
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =new List<string>()
                    {
                        "http://localhost:1391/signin-oidc",
                    },
                    PostLogoutRedirectUris =new List<string>()
                    {
                        "http://localhost:1391/signout-callback-oidc",
                        "http://localhost:1391/Account/SignedOut",
                    },
                    AllowedScopes =new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        MyProjectIdsConst.MyProjectScopes.Roles,
                        "testApi"
                    }
                },

                new Client
                {
                    ClientId = "testJsImplicit",
                    ClientName = "JavaScript Implicit Client",
                    AllowedGrantTypes = new List<string>() {OidcConstants.GrantTypes.Implicit},
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true,

                    AllowedScopes =new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        MyProjectIdsConst.MyProjectScopes.Roles,
                        "testApi"
                    },

                    RedirectUris =new List<string>()
                    {
                        "http://localhost:21575/index.html",
                        "http://localhost:21575/silent_renew.html",
                        "http://localhost:21575/popup_callback.html",
                    },

                    PostLogoutRedirectUris = new List<string>(){"http://localhost:21575/index.html",},
                    AllowedCorsOrigins =new List<string>() {"http://localhost:21575",},

                    AccessTokenLifetime = 3600,
                    AccessTokenType = (int)Ids4Enums.AccessTokenType.Jwt
                },

                ///////////////////////////////////////////
                // Skoruba.IdentityServer4.Admin Client
                //////////////////////////////////////////
                new Client
                {

                    ClientId = AdminPortalConsts.OidcClientId,
                    ClientName = AdminPortalConsts.OidcClientId,
                    ClientUri = AdminPortalConsts.IdentityAdminBaseUrl,

                    AllowedGrantTypes = new List<string>() {OidcConstants.GrantTypes.Implicit},
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =new List<string>() { $"{AdminPortalConsts.IdentityAdminBaseUrl}/signin-oidc"},
                    FrontChannelLogoutUri = $"{AdminPortalConsts.IdentityAdminBaseUrl}/signout-oidc",
                    PostLogoutRedirectUris = new List<string>() { $"{AdminPortalConsts.IdentityAdminBaseUrl}/signout-callback-oidc"},
                    AllowedCorsOrigins =new List<string>() { AdminPortalConsts.IdentityAdminBaseUrl },

                    AllowedScopes =new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        MyProjectIdsConst.MyProjectScopes.Roles,
                    }
                },

            };
        }
    }
}