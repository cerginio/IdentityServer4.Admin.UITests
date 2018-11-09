using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Admin.UITests.Models.IdentityModel;
using IdentityServer4.Admin.UITests.Pages.Admin;
using IdentityServer4.Admin.UITests.Pages.Admin.EditClient.Pages;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Steps.Administration
{
    internal class ClientInformationBatchUpdateSteps : AdminStepsBase
    {
        private readonly ClientRegistrationSteps _registrationSteps;

        public ClientInformationBatchUpdateSteps(IWebDriver driver, string adminRoot) : base(driver, adminRoot)
        {
            _registrationSteps = new ClientRegistrationSteps(driver, adminRoot);
        }

        public void ResetSecretsForClientsByGrantType(string[] clients, string secret, string grantType)
        {
            var homePage = GoToHomePage();
            homePage.ClientLink.Click();
            ClientsListPage clientListPage = new ClientsListPage(Driver);

            foreach (var clientId in clients)
            {
                clientListPage.Navigate();
                var isClientExists = clientListPage.OpenItemForEditIfExists(clientId);

                if (!isClientExists)
                {
                    continue;
                }

                ClientDetailsEditPage editClientPage = new ClientDetailsEditPage(Driver);

                if (!editClientPage.BasicsTab.AllowedGrantTypes.SelectedTagsValues.Any(x => x == grantType/* "client_credentials"*/))
                {
                    continue;
                }

                _registrationSteps.ManageSecrets(new List<ClientSecret>() { new ClientSecret(secret) }, editClientPage);

                editClientPage.SaveClientBtn.Click();
            }
        }
    }
}
