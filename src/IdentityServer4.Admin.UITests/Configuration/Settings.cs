using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityServer4.Admin.UITests.Configuration
{
    internal static class Settings
    {
        private static TestContext _testContext;

        public static void SetTestContext(TestContext context)
        {
            if (!context.Properties.ContainsKey("SeleniumDriver"))
                throw new Exception("Parameters from .runsettings file are missing. In VisualStudio Test->Test Settings->Select Test Settings File.");

            _testContext = context;
        }

        public static string Get(string key)
        {
            if (_overridedProperties.ContainsKey(key))
            {
                return _overridedProperties[key];
            }

            return _testContext.Properties[key] as string;
        }

        static readonly Dictionary<string, string> _overridedProperties = new Dictionary<string, string>();

        public static void Override(string key, string value)
        {
            if (_overridedProperties.ContainsKey(key))
            {
                _overridedProperties[key] = value;
            }
            else
            {
                _overridedProperties.Add(key, value);
            }
        }

        public static void ResetToDefault()
        {
            _overridedProperties.Clear();
        }
    }
}
