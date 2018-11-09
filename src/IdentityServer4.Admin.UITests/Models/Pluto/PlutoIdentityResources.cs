using Pluto.Test.UI.Steps.Administration.Models.IdentityModel;

namespace Pluto.Test.UI.Steps.Administration.Models.Pluto
{
    public static class MyProjectIdentityResourcesStub
    {
        private const string DefaultPrefix = "test.";

        public class Roles : IdentityResource
        {
            public Roles(string prefix = DefaultPrefix)
            {
                Name = $"{prefix}{MyProjectIdsConst.MyProjectScopes.Roles}";
                DisplayName = $"{prefix}User authorization Roles";
                Required = false;

                UserClaims = new[] { "role" };
            }
        }
    }
}