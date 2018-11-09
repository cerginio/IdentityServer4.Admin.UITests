using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Admin.UITests.Html.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Pluto.Test.UI.Api.Admin;
using Pluto.Test.UI.Api.Admin.EditClient.Pages;
using Pluto.Test.UI.Api.TestCoreMvc;
using Pluto.Test.UI.Steps.Administration.Models.IdentityModel;

namespace Pluto.Test.UI.Steps.Administration
{
    internal class ClientInformationUpdateSteps : AdminStepsBase
    {
        private readonly ClientRegistrationSteps _registrationSteps;

        public ClientInformationUpdateSteps(IWebDriver driver, string adminRoot) : base(driver, adminRoot)
        {
            _registrationSteps = new ClientRegistrationSteps(driver, adminRoot);
        }


        public void PrepareMigratedClientForAuthTest(string clientId)
        {
            var homePage = GoToHomePage();
            homePage.ClientLink.Click();
            ClientsListPage clientListPage = new ClientsListPage(Driver);
            var isClientExists = clientListPage.OpenItemForEditIfExists(clientId);

            if (!isClientExists)
            {
              Assert.Fail($"not migrated client: {clientId}");
            }

            // Edit new client
            ClientDetailsEditPage editClientPage = new ClientDetailsEditPage(Driver);

            if (!editClientPage.BasicsTab.RedirectUris.SelectedTagsValues.Any(x => x == TestClientUrl.PostResultUri))
            {
                editClientPage.BasicsTab.RedirectUris.AddItem(TestClientUrl.PostResultUri);
            }

            //editClientPage.ConsentScreenTab.RequireConsent.Checked = specification.RequireConsent;
            _registrationSteps.SaveClient(editClientPage);

            AddSecret(new List<ClientSecret>(){new ClientSecret("secret")}, editClientPage, 2);
        }

        public void UpdateClientBasicsAndSecrets(Client specification)
        {
            var homePage = GoToHomePage();
            homePage.ClientLink.Click();
            ClientsListPage clientListPage = new ClientsListPage(Driver);

            var isClientExists = clientListPage.OpenItemForEditIfExists(specification.ClientId);

            if (!isClientExists)
            {           
                // Create new client
                _registrationSteps.CreateNewClient(specification, clientListPage);
            }

            // Edit new client
            ClientDetailsEditPage editClientPage = new ClientDetailsEditPage(Driver);
            editClientPage.NameTab.ClientName.EnterText(specification.ClientName);
            _registrationSteps.SetupBasicsTab(specification, editClientPage);
            editClientPage.ConsentScreenTab.RequireConsent.Checked = specification.RequireConsent;
            _registrationSteps.SaveClient(editClientPage);
            AddSecret(specification.ClientSecrets, editClientPage, 2);
        }



        internal void AddSecret(List<ClientSecret> clientSecrets, ClientDetailsEditPage editClientPage, int totalSecretsLimit)
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
                    if (secretsPage.GetItems().Count >= totalSecretsLimit) break;

                    var secret = clientSecrets[i];
                    secretsPage.Value.EnterText(secret.Value);
                    secretsPage.Type.SelectByValue(secret.Type);
                    secretsPage.HashType.SelectByValue(
                        $"{((int)Ids4Enums.HashType.Sha512).ToString()}"); // 512 by default
                    secretsPage.AddSecret.Click();
                }
            }

            secretsPage.BackToClient.Click();
        }

    }
}