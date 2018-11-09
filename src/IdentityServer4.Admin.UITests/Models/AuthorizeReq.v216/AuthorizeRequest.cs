using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Pluto.Test.UI.IdentityModel216
{
    public class AuthorizeRequest
    {
        private readonly Uri _authorizeEndpoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:IdentityModel.Client.AuthorizeRequest" /> class.
        /// </summary>
        /// <param name="authorizeEndpoint">The authorize endpoint.</param>
        public AuthorizeRequest(Uri authorizeEndpoint)
        {
            this._authorizeEndpoint = authorizeEndpoint;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:IdentityModel.Client.AuthorizeRequest" /> class.
        /// </summary>
        /// <param name="authorizeEndpoint">The authorize endpoint.</param>
        public AuthorizeRequest(string authorizeEndpoint)
        {
            this._authorizeEndpoint = new Uri(authorizeEndpoint);
        }

        /// <summary>Creates URL based on key/value input pairs.</summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public string Create(IDictionary<string, string> values)
        {
            string str = string.Join("&", values.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>)(kvp => string.Format("{0}={1}", (object)WebUtility.UrlEncode(kvp.Key), (object)WebUtility.UrlEncode(kvp.Value)))).ToArray<string>());
            return (!this._authorizeEndpoint.IsAbsoluteUri ? string.Format("{0}?{1}", (object)this._authorizeEndpoint.OriginalString, (object)str) : string.Format("{0}?{1}", (object)this._authorizeEndpoint.AbsoluteUri, (object)str)).TrimEnd('?');
        }
    }
}