using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace IdentityServer4.Admin.UITests.Tests.Admin.Base
{
    public class ClaimsAssertionFunc
    {
        public static string[] _dynamicClaimTypes = new[] { "iss", "exp", "nbf", "nonce", "iat", "sid", "auth_time", "at_hash" };// "sub",
        // NOTE: sub claim is static for Ids4,
        // at_hash - new claim added to Ids4 29/08/2018


        public static Func<(bool, string, string)> ClaimsNotDiffer(Claim[] token1, Claim[] token2, params string[] excludeTypes)
        {
            var excludingDic = BuildExcludingDic(excludeTypes);

            var firstList = token1.Where(x => !excludingDic.ContainsKey(x.Type))
                .Select(x => $"{x.Type}: {x.Value}").ToList();

            var secondList = token2.Where(x => !excludingDic.ContainsKey(x.Type))
                .Select(x => $"{x.Type}: {x.Value}").ToList();

            string rawData1 = $"{Environment.NewLine}Token1 claims:{Environment.NewLine}" + string.Join($"{Environment.NewLine}", firstList.OrderBy(c => c));
            string rawData2 = $"{Environment.NewLine}Token2 claims:{Environment.NewLine}" + string.Join($"{Environment.NewLine}", secondList.OrderBy(c => c));

            return BuldListComparerFunc(firstList, secondList, null, rawData1 + Environment.NewLine + rawData2);
        }


        public static Func<(bool, string, string)> ClaimTypesNotDiffer(Claim[] token1, Claim[] token2, params string[] excludeTypes)
        {
            var excludingDic = BuildExcludingDic(excludeTypes);

            var statistics1 = token1.GroupBy(x => x.Type).ToDictionary(x => x.Key, x => x.Count());
            var statistics2 = token2.GroupBy(x => x.Type).ToDictionary(x => x.Key, x => x.Count());

            List<string> diff = new List<string>();

            var duplStat1 = statistics1.Where(x => x.Value > 1).ToList();// N
            var duplStat2 = statistics2.Where(x => x.Value > 1).ToList();// M

            if (duplStat1.Count > 0)
            {
                foreach (var s1 in duplStat1)
                {
                    var type1 = s1.Key;
                    // N vs 0
                    if (!statistics2.ContainsKey(type1))
                    {
                        diff.Add($"Type '{type1}' in token1 exists {s1.Value} times (more than 1 against 0 in token2).");
                    }
                    // N vs M
                    if (statistics2.ContainsKey(type1) && statistics2[type1] != s1.Value)
                    {
                        diff.Add($"Type '{type1}' in token2 exists {s1.Value} times against {statistics2[type1]} in token1.");
                    }
                }
            }

            if (duplStat2.Count > 0)
            {
                foreach (var s2 in duplStat2)
                {
                    var type2 = s2.Key;

                    // N vs 0
                    if (!statistics1.ContainsKey(type2))
                    {
                        diff.Add($"Type '{type2}' in token2 exists {s2.Value} times (more than 1 against 0 in token1).");
                    }

                    // N vs M
                    if (statistics1.ContainsKey(type2) && statistics1[type2] != s2.Value)
                    {
                        diff.Add($"Type '{type2}' in token1 exists {s2.Value} times  against {statistics1[type2] } in token2.");
                    }
                }
            }

            if (diff.Count == 0) diff = null;

            var firstList = token1.Where(x => !excludingDic.ContainsKey(x.Type))
                .Select(x => $"{x.Type}").ToList();

            var secondList = token2.Where(x => !excludingDic.ContainsKey(x.Type))
                .Select(x => $"{x.Type}").ToList();

            string rawData1 = $"{Environment.NewLine}Token1 claim types stat:{Environment.NewLine}" + string.Join($"{Environment.NewLine}", 
                                  statistics1.OrderByDescending(c => c.Value).ThenBy(c=>c.Key).Select(x=>$"'{x.Key}': {x.Value}"));

            string rawData2 = $"{Environment.NewLine}Token2 claim types stat:{Environment.NewLine}" + string.Join($"{Environment.NewLine}", 
                                  statistics2.OrderByDescending(c => c.Value).ThenBy(c => c.Key).Select(x=>$"'{x.Key}': {x.Value}"));

            return BuldListComparerFunc(firstList, secondList,  diff, rawData1 + Environment.NewLine + rawData2);

        }

        private static Dictionary<string, string> BuildExcludingDic(string[] excludeTypes)
        {
            var excludeList = new List<string>(_dynamicClaimTypes);

            if (excludeTypes != null && excludeTypes.Length > 0)
            {
                excludeList.AddRange(excludeTypes);
            }

            var excludingDic = excludeList.ToDictionary(x => x);
            return excludingDic;
        }


        private static Func<(bool, string, string)> BuldListComparerFunc(List<string> firstList, List<string> secondList,  List<string> diff, string context)
        {
            return () =>
            {
                List<string> diff1 = secondList.Except(firstList).ToList();
                List<string> diff2 = firstList.Except(secondList).ToList();

                var message = diff == null || diff.Count == 0
                    ? ""
                    : "diff Token1 vs Token2:"
                      + Environment.NewLine
                      + string.Join(Environment.NewLine, diff)
                      + Environment.NewLine;

                message += diff2.Count == 0
                    ? ""
                    : "Token1 claims not match in Token2:"
                      + Environment.NewLine
                      + string.Join(Environment.NewLine, diff2)
                      + Environment.NewLine;

                message += diff1.Count == 0
                    ? ""
                    : "Token2 claims not match in Token1:"
                      + Environment.NewLine
                      + string.Join(Environment.NewLine, diff1)
                      + Environment.NewLine;


                var diffDetected = (diff != null && diff.Count > 0) || diff1.Count > 0 || diff2.Count > 0;

                return (diffDetected, message, context);
            };
        }
    }
}