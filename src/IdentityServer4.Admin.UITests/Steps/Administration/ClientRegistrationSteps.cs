using System;
using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Admin.UITests.Html.Extensions;
using IdentityServer4.Admin.UITests.Models.IdentityModel;
using IdentityServer4.Admin.UITests.Pages.Admin;
using IdentityServer4.Admin.UITests.Pages.Admin.EditClient.Delete;
using IdentityServer4.Admin.UITests.Pages.Admin.EditClient.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Steps.Administration
{
    public class ClientRegistrationSteps : AdminStepsBase
    {

        public ClientRegistrationSteps(IWebDriver driver, string adminRoot) : base(driver, adminRoot)
        {
        }


        public void RegisterNewClientAccordingSpecification(Client specification)
        {
            var homePage = GoToHomePage();
            homePage.ClientLink.Click();

            ClientsListPage clientListPage = new ClientsListPage(Driver);

            var isClientExists = clientListPage.OpenItemForEditIfExists(specification.ClientId);

            if (isClientExists)
            {
                DeleteClient(specification);
            }

            // Create new client
            CreateNewClient(specification, clientListPage);

            var editClientPage = SetupExistingClient(specification);

            SaveClient(editClientPage);
        }

        private ClientDetailsEditPage SetupExistingClient(Client specification)
        {
            // Edit new client
            ClientDetailsEditPage editClientPage = new ClientDetailsEditPage(Driver);

            // NameTab
            editClientPage.NameTab.ClientId.EnterText(specification.ClientId);
            editClientPage.NameTab.ClientName.EnterText(specification.ClientName);

            // BasicsTab
            SetupBasicsTab(specification, editClientPage);

            // AuthenticationLogoutTab
            SetupAuthenticationLogoutTab(specification, editClientPage);

            // TokenTab
            SetupTokenTab(specification, editClientPage);

            // Consent
            SetupConsentTab(specification, editClientPage);

            SaveClient(editClientPage);

            // Claims
            ManageClaims(specification.Claims, editClientPage);

            // Secrets
            ManageSecrets(specification.ClientSecrets, editClientPage);

            // Properties - PoC
            var properties = new List<(string, string)>
            {
                ("owner", "Idm team / integration test"),
                ("test executed on", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}"),
                ("example", "example to be deleted")
            };

            if (specification.Properties != null)
            {
                properties.AddRange(specification.Properties.Select(x => (x.Key, x.Value)).ToArray());
            }

            ManageProperties(properties.ToArray(), editClientPage);
            return editClientPage;
        }


        internal void SaveClient(ClientDetailsEditPage editClientPage)
        {
            editClientPage.SaveClientBtn.Click();
            editClientPage.Navigate();
        }

        private void DeleteClient(Client specification)
        {
            ClientDetailsEditPage editClientPage = new ClientDetailsEditPage(Driver);
            Assert.AreEqual(specification.ClientId, editClientPage.NameTab.ClientId.Value);

            editClientPage.DeleteClientBtn.Click();
            ClientDeletePage deletePage = new ClientDeletePage(Driver);
            Assert.AreEqual(specification.ClientId, deletePage.ClientId.Value);
            Assert.AreEqual(specification.ClientName, deletePage.ClientName.Value);
            deletePage.DeleteClient.Click();
        }

        internal void CreateNewClient(Client specification, ClientsListPage clientList)
        {
            // Navigate to clients list page
            Driver.Url = $"{_adminRoot}/Configuration/Clients";

            clientList.CreateNewItemBtn.Click();

            ClientRegistrationPage newClientRegistration = new ClientRegistrationPage(Driver);
            newClientRegistration.ClientId.EnterText(specification.ClientId);
            newClientRegistration.ClientName.EnterText(specification.ClientName);

            newClientRegistration.MachineTemplate.Click();
            newClientRegistration.MobileHybridTemplate.Click();
            newClientRegistration.SPAImplicitHybridTemplate.Click();
            newClientRegistration.WebAppServerHybridTemplate.Click();
            newClientRegistration.WebAppServerImplicitTemplate.Click();
            newClientRegistration.EmptyTemplate.Click();

            newClientRegistration.SaveClient.Click();
        }

        internal void SetupBasicsTab(Client specification, ClientDetailsEditPage editClientPage)
        {
            // BasicsTab
            editClientPage.BasicsTab.Description.EnterText(specification.Description ?? specification.ClientName);
            editClientPage.BasicsTab.ProtocolType.SelectByValue(specification.ProtocolType);

            // Test switch
            //editClientPage.BasicsTab.RequireClientSecret.Switch();
            //editClientPage.BasicsTab.RequireClientSecret.On();
            //editClientPage.BasicsTab.RequireClientSecret.Off();

            // BasicsTab.Toggles
            editClientPage.BasicsTab.RequireClientSecret.Checked = specification.RequireClientSecret;
            editClientPage.BasicsTab.RequirePkce.Checked = specification.RequirePkce;
            editClientPage.BasicsTab.AllowPlainTextPkce.Checked = specification.AllowPlainTextPkce;
            editClientPage.BasicsTab.AllowAccessTokensViaBrowser.Checked = specification.AllowAccessTokensViaBrowser;
            editClientPage.BasicsTab.AllowOfflineAccess.Checked = specification.AllowOfflineAccess;


            // BasicsTab.Tags
            editClientPage.BasicsTab.AllowedScopes.ClearSelectedItems();
            editClientPage.BasicsTab.AllowedScopes.AddItems(specification.AllowedScopes.ToArray());

            editClientPage.BasicsTab.RedirectUris.ClearSelectedItems();
            editClientPage.BasicsTab.RedirectUris.AddItems(specification.RedirectUris.ToArray());

            editClientPage.BasicsTab.AllowedGrantTypes.ClearSelectedItems();
            editClientPage.BasicsTab.AllowedGrantTypes.AddItems(specification.AllowedGrantTypes.ToArray());
        }


        internal void SetupTokenTab(Client specification, ClientDetailsEditPage editClientPage)
        {
            // TokenTab
            // Toggles
            editClientPage.TokenTab.IdentityTokenLifetime.EnterText($"{specification.IdentityTokenLifetime}");
            editClientPage.TokenTab.AccessTokenLifetime.EnterText($"{specification.AccessTokenLifetime}");
            editClientPage.TokenTab.AuthorizationCodeLifetime.EnterText($"{specification.AuthorizationCodeLifetime}");
            editClientPage.TokenTab.AbsoluteRefreshTokenLifetime.EnterText($"{specification.AbsoluteRefreshTokenLifetime}");
            editClientPage.TokenTab.SlidingRefreshTokenLifetime.EnterText($"{specification.SlidingRefreshTokenLifetime}");

            // Select
            editClientPage.TokenTab.AccessTokenType.SelectByValue($"{specification.AccessTokenType}");
            editClientPage.TokenTab.RefreshTokenExpiration.SelectByValue($"{specification.RefreshTokenExpiration}");
            editClientPage.TokenTab.RefreshTokenUsage.SelectByValue($"{specification.RefreshTokenUsage}");

            // Tags
            editClientPage.TokenTab.AllowedCorsOrigins.ClearSelectedItems();
            editClientPage.TokenTab.AllowedCorsOrigins.AddItems(specification.AllowedCorsOrigins?.ToArray() ?? new string[] { });

            // Toggles
            editClientPage.TokenTab.UpdateAccessTokenClaimsOnRefresh.Checked = specification.UpdateAccessTokenClaimsOnRefresh;
            editClientPage.TokenTab.IncludeJwtId.Checked = specification.IncludeJwtId;
            editClientPage.TokenTab.AlwaysIncludeUserClaimsInIdToken.Checked = specification.AlwaysIncludeUserClaimsInIdToken;
            editClientPage.TokenTab.AlwaysSendClientClaims.Checked = specification.AlwaysSendClientClaims;

            // Input
            editClientPage.TokenTab.ClientClaimsPrefix.EnterText(specification.ClientClaimsPrefix ?? "");
            editClientPage.TokenTab.PairWiseSubjectSalt.EnterText(specification.PairWiseSubjectSalt ?? "");
        }

        private void SetupAuthenticationLogoutTab(Client specification, ClientDetailsEditPage editClientPage)
        {
            // AuthenticationLogoutTab
            editClientPage.AuthenticationLogoutTab.FrontChannelLogoutSessionRequired.Checked =
                specification.FrontChannelLogoutSessionRequired;
            editClientPage.AuthenticationLogoutTab.BackChannelLogoutSessionRequired.Checked =
                specification.BackChannelLogoutSessionRequired;
            editClientPage.AuthenticationLogoutTab.EnableLocalLogin.Checked = specification.EnableLocalLogin;

            editClientPage.AuthenticationLogoutTab.FrontChannelLogoutUri.EnterText(specification.FrontChannelLogoutUri ?? "");
            editClientPage.AuthenticationLogoutTab.BackChannelLogoutUri.EnterText(specification.BackChannelLogoutUri ?? "");


            editClientPage.AuthenticationLogoutTab.PostLogoutRedirectUris.ClearSelectedItems();
            editClientPage.AuthenticationLogoutTab.PostLogoutRedirectUris.AddItems(specification.PostLogoutRedirectUris?.ToArray() ?? new string[] { });

            editClientPage.AuthenticationLogoutTab.IdentityProviderRestrictions.ClearSelectedItems();
            editClientPage.AuthenticationLogoutTab.IdentityProviderRestrictions.AddItems(specification.IdentityProviderRestrictions?.ToArray() ?? new string[] { });
        }


        private void SetupConsentTab(Client specification, ClientDetailsEditPage editClientPage)
        {
            editClientPage.ConsentScreenTab.RequireConsent.Checked = specification.RequireConsent;
            editClientPage.ConsentScreenTab.AllowRememberConsent.Checked = specification.AllowRememberConsent;

            editClientPage.ConsentScreenTab.ClientUri.EnterText(specification.ClientUri ?? "");
            editClientPage.ConsentScreenTab.LogoUri.EnterText(specification.LogoUri ?? "");
        }



        private void ManageClaims(List<ClientClaim> claims, ClientDetailsEditPage editClientPage)
        {
            //if (specification.Claims == null) return;

            Assert.AreEqual("_blank", editClientPage.TokenTab.ManageClientClaims.GetAttribute("target"));
            editClientPage.TokenTab.ManageClientClaims.SetAttribute(Driver, "target", "_self");
            Assert.AreEqual("_self", editClientPage.TokenTab.ManageClientClaims.GetAttribute("target"));

            editClientPage.TokenTab.ManageClientClaims.Click();
            ClientClaimsPage clientClaimsPage = new ClientClaimsPage(Driver);

            if (claims != null)
            {
                for (var i = 0; i < claims.Count; i++)
                {
                    var claim = claims[i];
                    clientClaimsPage.ClaimType.AddItem(claim.Type);
                    clientClaimsPage.ClaimValue.EnterText(claim.Value);
                    clientClaimsPage.AddClientClaim.Click();

                    var topItem = clientClaimsPage.GetItem(1);
                    Assert.AreEqual(claim.Type, topItem.Type.Text);
                    Assert.AreEqual(claim.Value, topItem.Value.Text);
                }

                // Delete latest in the list, first added item - test (OK)
                //clientClaimsPage.DeleteItem(specification.Claims.Count);
            }

            clientClaimsPage.BackToClient.Click();
        }

        internal void ManageSecrets(List<ClientSecret> clientSecrets, ClientDetailsEditPage editClientPage)
        {

            Assert.AreEqual("_blank", editClientPage.BasicsTab.ManageSecrets.GetAttribute("target"));
            editClientPage.BasicsTab.ManageSecrets.SetAttribute(Driver, "target", "_self");
            Assert.AreEqual("_self", editClientPage.BasicsTab.ManageSecrets.GetAttribute("target"));

            editClientPage.BasicsTab.ManageSecrets.Click();

            ClientSecretsPage secretsPage = new ClientSecretsPage(Driver);
            if (clientSecrets?.Count > 0)
            {
                for (var i = 0; i < clientSecrets.Count; i++)
                {
                    var secret = clientSecrets[i];
                    secretsPage.Value.EnterText(secret.Value);
                    secretsPage.Type.SelectByValue(secret.Type);
                    secretsPage.HashType.SelectByValue(
                        $"{((int)Ids4Enums.HashType.Sha512).ToString()}"); // 512 by default
                    secretsPage.AddSecret.Click();

                    var topItem = secretsPage.GetItem(1);
                    Assert.AreEqual(secret.Type, topItem.Type.Text);
                    Assert.AreEqual(88, topItem.Value.Text.Length); // 256 => 44, 512 => 88, 
                }
            }
            // Delete latest in the list, first added item - test (OK)
            //secretsPage.DeleteItem(specification.ClientSecrets.Count);

            secretsPage.BackToClient.Click();
        }


        private void ManageProperties((string, string)[] properties, ClientDetailsEditPage editClientPage)
        {
            Assert.AreEqual("_blank", editClientPage.BasicsTab.ManageClientProperties.GetAttribute("target"));
            editClientPage.BasicsTab.ManageClientProperties.SetAttribute(Driver, "target", "_self");
            Assert.AreEqual("_self", editClientPage.BasicsTab.ManageClientProperties.GetAttribute("target"));

            editClientPage.BasicsTab.ManageClientProperties.Click();
            ClientPropertiesPage propertiesPage = new ClientPropertiesPage(Driver);
            if (properties?.Length > 0)
            {
                for (var i = 0; i < properties.Length; i++)
                {
                    var p = properties[i];
                    propertiesPage.Key.EnterText(p.Item1);
                    propertiesPage.Value.EnterText(p.Item2);


                    propertiesPage.AddProperty.Click();

                    var topItem = propertiesPage.GetItem(1);
                    Assert.AreEqual(p.Item1, topItem.Key.Text);
                    Assert.AreEqual(p.Item2, topItem.Value.Text);
                }

                propertiesPage.DeleteItem(properties.Length);
            }

            propertiesPage.BackToClient.Click();
        }

    }
}
