using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IdentityServer4.Admin.UITests.Models.IdentityModelv216.AuthReq
{
    public static class AuthorizeRequestExtensions
    {
        /// <summary>Creates the authorize URL.</summary>
        /// <param name="request">The request.</param>
        /// <param name="values">The values (either using a string Dictionary or an object's properties).</param>
        /// <returns></returns>
        public static string Create(this AuthorizeRequest request, object values)
        {
            return request.Create(
                (IDictionary<string, string>)AuthorizeRequestExtensions.ObjectToDictionary(
                    values));
        }

        /// <summary>Creates the authorize URL.</summary>
        /// <param name="request">The request.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="responseType">The response type.</param>
        /// <param name="scope">The scope.</param>
        /// <param name="redirectUri">The redirect URI.</param>
        /// <param name="state">The state.</param>
        /// <param name="nonce">The nonce.</param>
        /// <param name="loginHint">The login hint.</param>
        /// <param name="acrValues">The acr values.</param>
        /// <param name="prompt">The prompt.</param>
        /// <param name="responseMode">The response mode.</param>
        /// <param name="codeChallenge">The code challenge.</param>
        /// <param name="codeChallengeMethod">The code challenge method.</param>
        /// <param name="extra">Extra parameters.</param>
        /// <returns></returns>
        public static string CreateAuthorizeUrl(this AuthorizeRequest request, string clientId, string responseType,
            string scope = null, string redirectUri = null, string state = null, string nonce = null,
            string loginHint = null, string acrValues = null, string prompt = null, string responseMode = null,
            string codeChallenge = null, string codeChallengeMethod = null, object extra = null)
        {
            Dictionary<string, string> explicitValues = new Dictionary<string, string>()
            {
                {
                    "client_id",
                    clientId
                },
                {
                    "response_type",
                    responseType
                }
            };
            if (!string.IsNullOrWhiteSpace(scope))
                explicitValues.Add(nameof(scope), scope);
            if (!string.IsNullOrWhiteSpace(redirectUri))
                explicitValues.Add("redirect_uri", redirectUri);
            if (!string.IsNullOrWhiteSpace(state))
                explicitValues.Add(nameof(state), state);
            if (!string.IsNullOrWhiteSpace(nonce))
                explicitValues.Add(nameof(nonce), nonce);
            if (!string.IsNullOrWhiteSpace(loginHint))
                explicitValues.Add("login_hint", loginHint);
            if (!string.IsNullOrWhiteSpace(acrValues))
                explicitValues.Add("acr_values", acrValues);
            if (!string.IsNullOrWhiteSpace(prompt))
                explicitValues.Add(nameof(prompt), prompt);
            if (!string.IsNullOrWhiteSpace(responseMode))
                explicitValues.Add("response_mode", responseMode);
            if (!string.IsNullOrWhiteSpace(codeChallenge))
                explicitValues.Add("code_challenge", codeChallenge);
            if (!string.IsNullOrWhiteSpace(codeChallengeMethod))
                explicitValues.Add("code_challenge_method", codeChallengeMethod);
            return request.Create((IDictionary<string, string>)AuthorizeRequestExtensions.Merge(explicitValues,
                AuthorizeRequestExtensions.ObjectToDictionary(extra)));
        }

        private static Dictionary<string, string> ObjectToDictionary(object values)
        {
            if (values == null)
                return (Dictionary<string, string>)null;
            Dictionary<string, string> dictionary1;
            if ((dictionary1 = values as Dictionary<string, string>) != null)
                return dictionary1;
            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
            foreach (PropertyInfo runtimeProperty in values.GetType().GetRuntimeProperties())
            {
                string str = runtimeProperty.GetValue(values) as string;
                if (!string.IsNullOrEmpty(str))
                    dictionary2.Add(runtimeProperty.Name, str);
            }

            return dictionary2;
        }

        private static Dictionary<string, string> Merge(Dictionary<string, string> explicitValues,
            Dictionary<string, string> additionalValues = null)
        {
            Dictionary<string, string> dictionary = explicitValues;
            if (additionalValues != null)
            {
                IEnumerable<KeyValuePair<string, string>> source =
                    explicitValues.Concat<KeyValuePair<string, string>>(
                        additionalValues.Where<KeyValuePair<string, string>>(
                            (Func<KeyValuePair<string, string>, bool>)(add =>
                                !explicitValues.ContainsKey(add.Key))));
                Func<KeyValuePair<string, string>, string> func =
                    (Func<KeyValuePair<string, string>, string>)(final => final.Key);
                Func<KeyValuePair<string, string>, string> keySelector = null;
                dictionary = source.ToDictionary<KeyValuePair<string, string>, string, string>(keySelector,
                    (Func<KeyValuePair<string, string>, string>)(final => final.Value));
            }

            return dictionary;
        }
    }
}