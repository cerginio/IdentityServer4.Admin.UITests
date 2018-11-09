using OpenQA.Selenium;
using Pluto.Test.UI.Api.Admin;
using Pluto.Test.UI.Api.Admin.EditClient.Pages;
using Pluto.Test.UI.Steps.Administration.Models.IdentityModel;
using System.Collections.Generic;
using System.Linq;

namespace Pluto.Test.UI.Steps.Administration
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
