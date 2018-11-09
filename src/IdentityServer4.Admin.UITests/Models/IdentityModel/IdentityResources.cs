using System.Linq;
using IdentityModel;

namespace Pluto.Test.UI.Steps.Administration.Models.IdentityModel
{
    public static class IdentityResourcesStub
    {
        private const string DefaultPrefix = "test.";
        /// <summary>
        /// Models the standard openid scope
        /// </summary>
        /// <seealso cref="IdentityResource" />
        public class OpenId : IdentityResource
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OpenId"/> class.
            /// </summary>
            public OpenId(string prefix = DefaultPrefix)
            {
                Name = $"{prefix}{IdentityServerConstants.StandardScopes.OpenId}";
                DisplayName = $"{prefix} Your user identifier";
                Required = true;
                UserClaims.Add(JwtClaimTypes.Subject);
            }
        }

        /// <summary>
        /// Models the standard profile scope
        /// </summary>
        /// <seealso cref="IdentityResource" />
        public class Profile : IdentityResource
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Profile"/> class.
            /// </summary>
            public Profile(string prefix = DefaultPrefix)
            {
                Name = $"{prefix}{IdentityServerConstants.StandardScopes.Profile}";
                DisplayName = $"{prefix} User profile";
                Description = "Your user profile information (first name, last name, etc.)";
                Emphasize = true;
                UserClaims = Constants.ScopeToClaimsMapping[IdentityServerConstants.StandardScopes.Profile].ToList();
            }
        }

        /// <summary>
        /// Models the standard email scope
        /// </summary>
        /// <seealso cref="IdentityResource" />
        public class Email : IdentityResource
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Email"/> class.
            /// </summary>
            public Email(string prefix = DefaultPrefix)
            {
                Name = $"{prefix}{IdentityServerConstants.StandardScopes.Email}";
                DisplayName = $"{prefix} Your email address";
                Emphasize = true;
                UserClaims = (Constants.ScopeToClaimsMapping[IdentityServerConstants.StandardScopes.Email].ToList());
            }
        }

        /// <summary>
        /// Models the standard phone scope
        /// </summary>
        /// <seealso cref="IdentityResource" />
        public class Phone : IdentityResource
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Phone"/> class.
            /// </summary>
            public Phone(string prefix = DefaultPrefix)
            {
                Name = $"{prefix}{IdentityServerConstants.StandardScopes.Phone}";
                DisplayName = $"{prefix} Your phone number";
                Emphasize = true;
                UserClaims = Constants.ScopeToClaimsMapping[IdentityServerConstants.StandardScopes.Phone].ToList();
            }
        }

        /// <summary>
        /// Models the standard address scope
        /// </summary>
        /// <seealso cref="IdentityResource" />
        public class Address : IdentityResource
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Address"/> class.
            /// </summary>
            public Address(string prefix = DefaultPrefix)
            {
                Name = $"{prefix}{IdentityServerConstants.StandardScopes.Address}";
                DisplayName = $"{prefix} Your postal address";
                Emphasize = true;
                UserClaims = Constants.ScopeToClaimsMapping[IdentityServerConstants.StandardScopes.Address].ToList();
            }
        }
    }

}
