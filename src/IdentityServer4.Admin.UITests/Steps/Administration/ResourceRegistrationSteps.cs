using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Pluto.Test.UI.Api.Admin;
using Pluto.Test.UI.Api.Admin.EditResource;
using Pluto.Test.UI.Api.Admin.EditResource.Delete;
using Pluto.Test.UI.Api.Admin.EditResource.Pages;
using Pluto.Test.UI.Steps.Administration.Models.IdentityModel;

namespace Pluto.Test.UI.Steps.Administration
{
    public class ResourceRegistrationSteps : AdminStepsBase
    {
        public ResourceRegistrationSteps(IWebDriver driver, string adminRoot) : base(driver, adminRoot)
        {
        }

        public void RegisteredResourceAccordingSpecification(ApiResource specification)
        {
            var homePage = GoToHomePage();
            homePage.ApiResourceLink.Click();

            ApiResourcesListPage resourcesList = new ApiResourcesListPage(Driver);

            var isClientExists = resourcesList.OpenItemForEditIfExists(specification.Name);

            if (isClientExists)
            {
                DeleteApiResource(specification);
            }

            CreateApiResource(specification, resourcesList);
        }




        public void RegisteredResourceAccordingSpecification(IdentityResource specification)
        {
            var homePage = GoToHomePage();
            homePage.IdentityResourceLink.Click();

            IdentityResourcesListPage resourcesListPage = new IdentityResourcesListPage(Driver);

            var isClientExists = resourcesListPage.OpenItemForEditIfExists(specification.Name);

            if (isClientExists)
            {
                DeleteIdentityResource(specification);
            }

            CreateIdentityResource(specification, resourcesListPage);
        }





        private void CreateIdentityResource(IdentityResource specification, IdentityResourcesListPage resourcesList)
        {
            // Navigate to clients list page
            Driver.Url = $"{_adminRoot}/Configuration/IdentityResources";
            resourcesList.CreateNewItemBtn.Click();

            IdentityResourceDetailsEditPage newResourceRegistration = new IdentityResourceDetailsEditPage(Driver);
            newResourceRegistration.Name.EnterText(specification.Name);
            newResourceRegistration.DisplayName.EnterText(specification.DisplayName);
            newResourceRegistration.Description.EnterText(specification.Description ?? specification.DisplayName ?? specification.Name);

            newResourceRegistration.Enabled.Checked = specification.Enabled;
            newResourceRegistration.Emphasize.Checked = specification.Emphasize;
            newResourceRegistration.Required.Checked = specification.Required;
            newResourceRegistration.ShowInDiscoveryDocument.Checked = specification.ShowInDiscoveryDocument;

            newResourceRegistration.UserClaims.AddItems(specification.UserClaims.ToArray());

            newResourceRegistration.SaveResourceBtn.Click();
        }

        private void CreateApiResource(ApiResource specification, ApiResourcesListPage resourcesList)
        {
            // Navigate to clients list page
            Driver.Url = $"{_adminRoot}/Configuration/ApiResources";
            resourcesList.CreateNewItemBtn.Click();

            ApiResourceDetailsEditPage newResourceRegistration = new ApiResourceDetailsEditPage(Driver);
            newResourceRegistration.Name.EnterText(specification.Name);
            newResourceRegistration.DisplayName.EnterText(specification.DisplayName);
            newResourceRegistration.Description.EnterText(specification.Description ?? specification.DisplayName ?? specification.Name);

            newResourceRegistration.Enabled.Checked = specification.Enabled;
            newResourceRegistration.UserClaims.AddItems(specification.UserClaims.ToArray());

            newResourceRegistration.SaveApiResourceBtn.Click();
            resourcesList.OpenItemForEditIfExists(specification.Name);

            newResourceRegistration.ManageApiScopes.Click();

            ApiScopesPage scopesPage = new ApiScopesPage(Driver);
            foreach (var scope in specification.Scopes)
            {
                scopesPage.Name.EnterText(scope.Name);
                scopesPage.Description.EnterText(scope.Description);
                scopesPage.DisplayName.EnterText(scope.DisplayName);
                scopesPage.Emphasize.Checked = scope.Emphasize;
                scopesPage.Required.Checked = scope.Required;
                scopesPage.UserClaims.AddItems(scope.UserClaims.ToArray());
                scopesPage.SaveApiScope.Click();


                var topItem = scopesPage.GetItem(1);
                Assert.AreEqual(scope.Name, topItem.Name.Text);

                // Test -OK
                topItem.EditBtn.Click();

                Assert.AreEqual(scope.Name, scopesPage.Name.Value);
                Assert.AreEqual(scope.Description, scopesPage.Description.Value);
                Assert.AreEqual(scope.DisplayName, scopesPage.DisplayName.Value);
                Assert.AreEqual(scope.Emphasize, scopesPage.Emphasize.Checked);
                Assert.AreEqual(scope.Required, scopesPage.Required.Checked);

                var existingClaims = scopesPage.UserClaims.SelectedTagsValues;

                foreach (var uc in scope.UserClaims)
                {
                    Assert.IsTrue(existingClaims.Contains(uc));
                }

                scopesPage.SaveApiScope.Click();
            }

            scopesPage.BackToResource.Click();
            newResourceRegistration.SaveApiResourceBtn.Click();
            resourcesList.OpenItemForEditIfExists(specification.Name);

            newResourceRegistration.ManageApiSecrets.Click();

            ApiSecretsPage secretsPage = new ApiSecretsPage(Driver);
            foreach (var secret in specification.ApiSecrets)
            {
                secretsPage.Value.EnterText(secret.Value);
                secretsPage.Type.SelectByText(secret.Type);
                secretsPage.HashType.SelectByText("Sha512");
                secretsPage.AddSecret.Click();

                var topItem = secretsPage.GetItem(1);
                Assert.AreEqual(secret.Type, topItem.Type.Text);
                Assert.AreEqual(88, topItem.Value.Text.Length);

            }
            secretsPage.BackToResource.Click();
        }


        private void DeleteApiResource(ApiResource specification)
        {
            ApiResourceDetailsEditPage editApiResPage = new ApiResourceDetailsEditPage(Driver);
            Assert.AreEqual(specification.Name, editApiResPage.Name.Value);

            editApiResPage.DeleteApiResourceBtn.Click();

            ApiResourceDeletePage deletePage = new ApiResourceDeletePage(Driver);
            Assert.AreEqual(specification.Name, deletePage.Name.Value);
            deletePage.DeleteResource.Click();
        }

        private void DeleteIdentityResource(IdentityResource specification)
        {
            IdentityResourceDetailsEditPage editApiResPage = new IdentityResourceDetailsEditPage(Driver);
            Assert.AreEqual(specification.Name, editApiResPage.Name.Value);

            editApiResPage.DeleteResourceBtn.Click();

            IdentityResourceDeletePage deletePage = new IdentityResourceDeletePage(Driver);
            Assert.AreEqual(specification.Name, deletePage.Name.Value);
            deletePage.DeleteResource.Click();
        }


    }
}