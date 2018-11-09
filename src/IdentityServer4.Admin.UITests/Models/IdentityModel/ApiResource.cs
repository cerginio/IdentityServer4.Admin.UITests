using System;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer4.Admin.UITests.Models.IdentityModel
{
    public class ApiResource : Resource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResource"/> class.
        /// </summary>
        public ApiResource()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResource"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ApiResource(string name)
            : this(name, name, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResource"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        public ApiResource(string name, string displayName)
            : this(name, displayName, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResource"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="claimTypes">The claim types.</param>
        public ApiResource(string name, IEnumerable<string> claimTypes)
            : this(name, name, claimTypes)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResource"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="claimTypes">The claim types.</param>
        /// <exception cref="System.ArgumentNullException">name</exception>
        public ApiResource(string name, string displayName, IEnumerable<string> claimTypes)
        {
            if (name.IsMissing()) throw new ArgumentNullException(nameof(name));

            Name = name;
            DisplayName = displayName;

            Scopes.Add(new Scope(name, displayName));

            if (!claimTypes.IsNullOrEmpty())
            {
                foreach (var type in claimTypes)
                {
                    UserClaims.Add(type);
                }
            }
        }

        /// <summary>
        /// The API secret is used for the introspection endpoint. The API can authenticate with introspection using the API name and secret.
        /// </summary>
        public ICollection<Secret> ApiSecrets { get; set; } = new HashSet<Secret>();

        /// <summary>
        /// An API must have at least one scope. Each scope can have different settings.
        /// </summary>
        public ICollection<Scope> Scopes { get; set; } = new HashSet<Scope>();

        internal ApiResource CloneWithScopes(IEnumerable<Scope> scopes)
        {
            return new ApiResource
            {
                Enabled = Enabled,
                Name = Name,
                ApiSecrets = ApiSecrets,
                Scopes = new HashSet<Scope>(scopes.ToArray()),
                UserClaims = UserClaims
            };
        }
    }

    public class Scope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Scope"/> class.
        /// </summary>
        public Scope()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Scope"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Scope(string name)
            : this(name, name, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Scope"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        public Scope(string name, string displayName)
            : this(name, displayName, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Scope"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="claimTypes">The user-claim types.</param>
        public Scope(string name, IEnumerable<string> claimTypes)
            : this(name, name, claimTypes)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Scope"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="claimTypes">The user-claim types.</param>
        /// <exception cref="System.ArgumentNullException">name</exception>
        public Scope(string name, string displayName, IEnumerable<string> claimTypes)
        {
            if (name.IsMissing()) throw new ArgumentNullException(nameof(name));

            Name = name;
            DisplayName = displayName;

            if (!claimTypes.IsNullOrEmpty())
            {
                foreach (var type in claimTypes)
                {
                    UserClaims.Add(type);
                }
            }
        }

        /// <summary>
        /// Name of the scope. This is the value a client will use to request the scope.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display name. This value will be used e.g. on the consent screen.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Description. This value will be used e.g. on the consent screen.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Specifies whether the user can de-select the scope on the consent screen. Defaults to false.
        /// </summary>
        public bool Required { get; set; } = false;

        /// <summary>
        /// Specifies whether the consent screen will emphasize this scope. Use this setting for sensitive or important scopes. Defaults to false.
        /// </summary>
        public bool Emphasize { get; set; } = false;

        /// <summary>
        /// Specifies whether this scope is shown in the discovery document. Defaults to true.
        /// </summary>
        public bool ShowInDiscoveryDocument { get; set; } = true;

        /// <summary>
        /// List of user-claim types that should be included in the access token.
        /// </summary>
        public ICollection<string> UserClaims { get; set; } = new HashSet<string>();
    }


    public class IdentityResource : Resource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityResource"/> class.
        /// </summary>
        public IdentityResource()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityResource"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="claimTypes">The claim types.</param>
        public IdentityResource(string name, IEnumerable<string> claimTypes)
            : this(name, name, claimTypes)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityResource"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="claimTypes">The claim types.</param>
        /// <exception cref="System.ArgumentNullException">name</exception>
        /// <exception cref="System.ArgumentException">Must provide at least one claim type - claimTypes</exception>
        public IdentityResource(string name, string displayName, IEnumerable<string> claimTypes)
        {
            if (name.IsMissing()) throw new ArgumentNullException(nameof(name));
            if (claimTypes.IsNullOrEmpty()) throw new ArgumentException("Must provide at least one claim type", nameof(claimTypes));

            Name = name;
            DisplayName = displayName;

            foreach (var type in claimTypes)
            {
                UserClaims.Add(type);
            }
        }

        /// <summary>
        /// Specifies whether the user can de-select the scope on the consent screen (if the consent screen wants to implement such a feature). Defaults to false.
        /// </summary>
        public bool Required { get; set; } = false;

        /// <summary>
        /// Specifies whether the consent screen will emphasize this scope (if the consent screen wants to implement such a feature). 
        /// Use this setting for sensitive or important scopes. Defaults to false.
        /// </summary>
        public bool Emphasize { get; set; } = false;

        /// <summary>
        /// Specifies whether this scope is shown in the discovery document. Defaults to true.
        /// </summary>
        public bool ShowInDiscoveryDocument { get; set; } = true;
    }

    public abstract class Resource
    {
        /// <summary>
        /// Indicates if this resource is enabled. Defaults to true.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// The unique name of the resource.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display name of the resource.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Description of the resource.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// List of accociated user claims that should be included when this resource is requested.
        /// </summary>
        public ICollection<string> UserClaims { get; set; } = new HashSet<string>();
    }

    public static class IEnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            if (list == null)
            {
                return true;
            }

            if (!list.Any())
            {
                return true;
            }

            return false;
        }

        public static bool HasDuplicates<T, TProp>(this IEnumerable<T> list, Func<T, TProp> selector)
        {
            var d = new HashSet<TProp>();
            foreach (var t in list)
            {
                if (!d.Add(selector(t)))
                {
                    return true;
                }
            }
            return false;
        }
    }

}