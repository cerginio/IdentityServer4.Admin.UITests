using IdentityServer4.Admin.UITests.Models.IdentityModel;

namespace IdentityServer4.Admin.UITests.Models.Pluto
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