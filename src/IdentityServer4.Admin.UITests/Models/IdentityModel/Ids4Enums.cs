namespace Pluto.Test.UI.Steps.Administration.Models.IdentityModel
{
    public class Ids4Enums
    {
        public enum AccessTokenType
        {
            Jwt = 0,
            Reference = 1,
        }

        public enum TokenExpiration
        {
            Sliding = 0,
            Absolute = 1,
        }

        public enum TokenUsage
        {
            ReUse = 0,
            OneTimeOnly = 1,
        }

        public enum HashType
        {
            Sha256 = 0,
            Sha512 = 1
        }
    }
}
