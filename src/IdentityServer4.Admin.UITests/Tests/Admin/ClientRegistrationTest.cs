using System.Linq;
using IdentityServer4.Admin.UITests.Configuration;
using IdentityServer4.Admin.UITests.Models;
using IdentityServer4.Admin.UITests.Steps.Administration;
using IdentityServer4.Admin.UITests.Tests.Admin.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityServer4.Admin.UITests.Tests.Admin
{
    [TestClass]
    public class ClientRegistrationTest : SeleniumTest
    {
        private string GetAdminRoot()
        {
            var adminRoot = StsUrl.AdminRootUrl;
            if (string.IsNullOrEmpty(adminRoot))
            {
                Assert.Inconclusive();
            }
            return adminRoot;
        }

        private ResourceRegistrationSteps _resourceRegistrationSteps;
        private ClientRegistrationSteps _clientRegistrationSteps;
        private ClientValidationSteps _clientValidationSteps;

        private readonly TestClientsSpecifications _testClients;

        public ClientRegistrationTest() : base()
        {
            _testClients = new TestClientsSpecifications();
        }


        [DataTestMethod]
        [DataRow("testClient")]
        [DataRow("testMvc")]
        public void TestMvcClientMustBeRegistered(string clientId)
        {
            ClientRegistrationAndValidationScenario(clientId);
        }



        // Scenario
        private void ClientRegistrationAndValidationScenario(string clientId)
        {
            var adminRoot = GetAdminRoot();

            _clientRegistrationSteps = new ClientRegistrationSteps(Driver, adminRoot);
            _clientValidationSteps = new ClientValidationSteps(Driver, adminRoot);

            var clients = _testClients.GetClientCopies();
            var client = clients.FirstOrDefault(x => x.ClientId == clientId);
            if (client == null) Assert.Inconclusive($"client #{clientId} not found");

            // Register
            var homePage = _clientRegistrationSteps.GoToHomePage();
            homePage.ClientLink.Click();
            _clientRegistrationSteps.RegisterNewClientAccordingSpecification(client);

            // Validate
            homePage = _clientRegistrationSteps.GoToHomePage();
            homePage.ClientLink.Click();

            _clientValidationSteps.ValidateIfClientMatchToSpecification(client);
        }



        [TestMethod]
        public void ResourcesMustBeRegistredAccordingSpecification()
        {
            var adminRoot = GetAdminRoot();

            _resourceRegistrationSteps = new ResourceRegistrationSteps(Driver, adminRoot);

            foreach (var apiResource in _testClients.GetApiResourcesCopies().Where(x => x.Name == "testapi1"))
            {
                _resourceRegistrationSteps.RegisteredResourceAccordingSpecification(apiResource);
            }

            foreach (var identityResource in _testClients.GetIdentityResourcesCopies())// .Where(x => x.Name == "test.openid")
            {
                _resourceRegistrationSteps.RegisteredResourceAccordingSpecification(identityResource);
            }
        }
    }
}
