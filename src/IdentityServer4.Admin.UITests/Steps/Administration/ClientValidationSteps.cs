using System.Linq;
using IdentityServer4.Admin.UITests.Html.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Pluto.Test.UI.Api.Admin;
using Pluto.Test.UI.Api.Admin.EditClient.Pages;
using Pluto.Test.UI.Steps.Administration.Models.IdentityModel;

namespace Pluto.Test.UI.Steps.Administration
{
    public class ClientValidationSteps: AdminStepsBase
    {

        public ClientValidationSteps(IWebDriver driver, string adminRoot):base(driver, adminRoot)
        {
        }

        public void ValidateIfClientMatchToSpecification(Client specification)
        {

            ClientsListPage clientListPage = new ClientsListPage(Driver);
            var isClientExists = clientListPage.OpenItemForEditIfExists(specification.ClientId);

            if (!isClientExists)
            {
                Assert.Fail($"Client {specification.ClientId} not exists");
            }

            ClientDetailsEditPage editClientPage = new ClientDetailsEditPage(Driver);

            Assert.AreEqual(specification.ClientId, editClientPage.NameTab.ClientId.Value);
            Assert.AreEqual(specification.ClientName, editClientPage.NameTab.ClientName.Value);


            ValidateBasicsTab(specification, editClientPage);
            ValidateAuthenticationLogoutTab(specification, editClientPage);
            ValidateTokenTab(specification, editClientPage);
            ValidateConsentTab(specification, editClientPage);

            ValidateClaims(specification, editClientPage);
            ValidateSecrets(specification, editClientPage);
            ValidateProperties(specification, editClientPage);

        }

        private void ValidateBasicsTab(Client specification, ClientDetailsEditPage editClientPage)
        {
            Assert.AreEqual(specification.ClientName, editClientPage.BasicsTab.Description.Value);
            Assert.AreEqual(specification.ProtocolType, editClientPage.BasicsTab.ProtocolType.Value);

            // BasicsTab.Toggles
            Assert.AreEqual(specification.RequireClientSecret, editClientPage.BasicsTab.RequireClientSecret.Checked);
            Assert.AreEqual(specification.RequirePkce, editClientPage.BasicsTab.RequirePkce.Checked);
            Assert.AreEqual(specification.AllowPlainTextPkce, editClientPage.BasicsTab.AllowPlainTextPkce.Checked);
            Assert.AreEqual(specification.AllowAccessTokensViaBrowser,
                editClientPage.BasicsTab.AllowAccessTokensViaBrowser.Checked);
            Assert.AreEqual(specification.AllowOfflineAccess, editClientPage.BasicsTab.AllowOfflineAccess.Checked);


            var scopes = editClientPage.BasicsTab.AllowedScopes.SelectedTagsValues;
            foreach (var scope in specification.AllowedScopes)
            {
                Assert.AreEqual(scope, scopes.FirstOrDefault(x => x == scope));
            }

            var redirectUris = editClientPage.BasicsTab.RedirectUris.SelectedTagsValues;
            foreach (var uri in specification.RedirectUris)
            {
                Assert.AreEqual(uri, redirectUris.FirstOrDefault(x => x == uri));
            }

            var grantTypes = editClientPage.BasicsTab.AllowedGrantTypes.SelectedTagsValues;
            foreach (var gType in specification.AllowedGrantTypes)
            {
                Assert.AreEqual(gType, grantTypes.FirstOrDefault(x => x == gType));
            }
        }

        private void ValidateAuthenticationLogoutTab(Client specification, ClientDetailsEditPage editClientPage)
        {
            // AuthenticationLogoutTab

            Assert.AreEqual(specification.FrontChannelLogoutSessionRequired, editClientPage.AuthenticationLogoutTab.FrontChannelLogoutSessionRequired.Checked);
            Assert.AreEqual(specification.BackChannelLogoutSessionRequired, editClientPage.AuthenticationLogoutTab.BackChannelLogoutSessionRequired.Checked);
            Assert.AreEqual(specification.EnableLocalLogin, editClientPage.AuthenticationLogoutTab.EnableLocalLogin.Checked);


            Assert.AreEqual(specification.FrontChannelLogoutUri ?? "", editClientPage.AuthenticationLogoutTab.FrontChannelLogoutUri.Value);
            Assert.AreEqual(specification.BackChannelLogoutUri ?? "", editClientPage.AuthenticationLogoutTab.BackChannelLogoutUri.Value);
            Assert.AreEqual(specification.FrontChannelLogoutSessionRequired, editClientPage.AuthenticationLogoutTab.FrontChannelLogoutSessionRequired.Checked);

            var logoutRedirectUris = editClientPage.AuthenticationLogoutTab.PostLogoutRedirectUris.SelectedTagsValues;
            if (specification.PostLogoutRedirectUris != null)
            {
                foreach (var uri in specification.PostLogoutRedirectUris)
                {
                    Assert.AreEqual(uri, logoutRedirectUris.FirstOrDefault(x => x == uri));
                }
            }

            var restrictions = editClientPage.AuthenticationLogoutTab.IdentityProviderRestrictions.SelectedTagsValues;
            if (specification.IdentityProviderRestrictions != null)
            {
                foreach (var restriction in specification.IdentityProviderRestrictions)
                {
                    Assert.AreEqual(restriction, restrictions.FirstOrDefault(x => x == restriction));
                }
            }
        }

        private void ValidateTokenTab(Client specification, ClientDetailsEditPage editClientPage)
        {
            // TokenTab
            // Inputs
            Assert.AreEqual($"{specification.IdentityTokenLifetime}", editClientPage.TokenTab.IdentityTokenLifetime.Value);
            Assert.AreEqual($"{specification.AccessTokenLifetime}", editClientPage.TokenTab.AccessTokenLifetime.Value);
            Assert.AreEqual($"{specification.AuthorizationCodeLifetime}", editClientPage.TokenTab.AuthorizationCodeLifetime.Value);
            Assert.AreEqual($"{specification.AbsoluteRefreshTokenLifetime}", editClientPage.TokenTab.AbsoluteRefreshTokenLifetime.Value);
            Assert.AreEqual($"{specification.SlidingRefreshTokenLifetime}", editClientPage.TokenTab.SlidingRefreshTokenLifetime.Value);

            // Selects
            Assert.AreEqual($"{specification.AccessTokenType}", editClientPage.TokenTab.AccessTokenType.Value);
            Assert.AreEqual($"{specification.RefreshTokenExpiration}", editClientPage.TokenTab.RefreshTokenExpiration.Value);
            Assert.AreEqual($"{specification.RefreshTokenUsage}", editClientPage.TokenTab.RefreshTokenUsage.Value);

            // Toggles
            Assert.AreEqual(specification.UpdateAccessTokenClaimsOnRefresh, editClientPage.TokenTab.UpdateAccessTokenClaimsOnRefresh.Checked);
            Assert.AreEqual(specification.IncludeJwtId, editClientPage.TokenTab.IncludeJwtId.Checked);
            Assert.AreEqual(specification.AlwaysIncludeUserClaimsInIdToken, editClientPage.TokenTab.AlwaysIncludeUserClaimsInIdToken.Checked);
            Assert.AreEqual(specification.AlwaysSendClientClaims, editClientPage.TokenTab.AlwaysSendClientClaims.Checked);

            // Inputs
            Assert.AreEqual($"{specification.ClientClaimsPrefix ?? ""}", editClientPage.TokenTab.ClientClaimsPrefix.Value);
            Assert.AreEqual($"{specification.PairWiseSubjectSalt ?? ""}", editClientPage.TokenTab.PairWiseSubjectSalt.Value);

            // Tags
            var corsOrigins = editClientPage.TokenTab.AllowedCorsOrigins.SelectedTagsValues;
            if (specification.AllowedCorsOrigins != null)
            {
                foreach (var cors in specification.AllowedCorsOrigins)
                {
                    Assert.AreEqual(cors, corsOrigins.FirstOrDefault(x => x == cors));
                }
            }

            // Link: TODO
        }

        private void ValidateConsentTab(Client specification, ClientDetailsEditPage editClientPage)
        {

            Assert.AreEqual($"{specification.ClientUri ?? ""}", editClientPage.ConsentScreenTab.ClientUri.Value);
            Assert.AreEqual($"{specification.LogoUri ?? ""}", editClientPage.ConsentScreenTab.LogoUri.Value);

            Assert.AreEqual(specification.RequireConsent, editClientPage.ConsentScreenTab.RequireConsent.Checked);
            Assert.AreEqual(specification.AllowRememberConsent, editClientPage.ConsentScreenTab.AllowRememberConsent.Checked);
        
        }



        private void ValidateClaims(Client specification, ClientDetailsEditPage editClientPage)
        {
            editClientPage.TokenTab.ManageClientClaims.SetAttribute(Driver, "target", "_self");

            editClientPage.TokenTab.ManageClientClaims.Click();
            ClientClaimsPage clientClaimsPage = new ClientClaimsPage(Driver);

            if (specification.Claims != null)
            {
                var items = clientClaimsPage.GetItems();

                foreach (var claim in specification.Claims)
                {
                    Assert.AreEqual(claim.Value, items.FirstOrDefault(x => x.Type.Text == claim.Type && x.Value.Text == claim.Value)?.Value.Text);
                }

            }

            clientClaimsPage.BackToClient.Click();
        }

        private void ValidateSecrets(Client specification, ClientDetailsEditPage editClientPage)
        {
            editClientPage.BasicsTab.ManageSecrets.SetAttribute(Driver, "target", "_self");
            editClientPage.BasicsTab.ManageSecrets.Click();

            ClientSecretsPage secretsPage = new ClientSecretsPage(Driver);

            if (specification.ClientSecrets != null)
            {
                var items = secretsPage.GetItems();
                Assert.AreEqual(specification.ClientSecrets.Count, items.Count);
                foreach (var secret in specification.ClientSecrets)
                {
                    // Value encrypted
                    //Assert.AreEqual(secret.Value, items.FirstOrDefault(x => x.Type.Text == secret.Type && x.Value.Text == secret.Value)?.Value.Text);
                    // Check if such type exists
                    Assert.AreEqual(secret.Type, items.FirstOrDefault(x => x.Type.Text == secret.Type && x.Value.Text.Length == 88)?.Type.Text);
                }
            }

            secretsPage.BackToClient.Click();
        }


        private void ValidateProperties(Client specification, ClientDetailsEditPage editClientPage)
        {
            editClientPage.BasicsTab.ManageClientProperties.SetAttribute(Driver, "target", "_self");
            editClientPage.BasicsTab.ManageClientProperties.Click();
            ClientPropertiesPage propertiesPage = new ClientPropertiesPage(Driver);

            if (specification.Properties != null)
            {
                var items = propertiesPage.GetItems();
                foreach (var property in specification.Properties)
                {
                    Assert.AreEqual(property.Value, items.FirstOrDefault(x => x.Key.Text == property.Key && x.Value.Text == property.Value)?.Value.Text);
                }
            }
            propertiesPage.BackToClient.Click();
        }
    }
}