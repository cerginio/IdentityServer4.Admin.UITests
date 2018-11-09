using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pluto.Test.UI.Tests.Base
{
    /// <summary>
    /// SoftAssertions
    /// https://automationrhapsody.com/soft-assertions-c-unit-testing-frameworks-mstest-nunit-xunit-net/
    /// </summary>
    public class SoftAssertions
    {
        private readonly List<SingleAssert> _verifications = new List<SingleAssert>();


        private readonly List<string> _generalContext = new List<string>();

        public void Add(string message, string expected, string actual)
        {
            _verifications.Add(new SingleAssert(message, expected, actual));
        }

        public void Add(string message, bool expected, bool actual)
        {
            Add(message, expected.ToString(), actual.ToString());
        }

        public void Add(string message, int expected, int actual)
        {
            Add(message, expected.ToString(), actual.ToString());
        }

        public void AddTrue(string message, bool actual)
        {
            _verifications.Add(new SingleAssert(message, true.ToString(), actual.ToString()));
        }


        public void AddFunc(string message, Func<(bool, string, string)> assertionFunc)
        {
            _verifications.Add(new SingleAssert(message, assertionFunc));
        }

        [DebuggerStepThrough]
        public void AssertAll()
        {
            var failed = _verifications.Where(v => v.Failed).ToList();
            string aggregatedMessage = Environment.NewLine + Environment.NewLine + "SoftAssertion context:" + Environment.NewLine + Environment.NewLine +
                                       string.Join($"{Environment.NewLine}", _generalContext) + Environment.NewLine;
            string singleAssertContext = null;
            for (int i = 0; i < failed.Count; i++)
            {
                // Failed Assert details
                aggregatedMessage += $"{Environment.NewLine}|#{i + 1}|{Environment.NewLine}{failed[i]}";

                // Failed Assert context
                if (failed[i].Context != null && failed[i].Context != singleAssertContext)
                {
                    singleAssertContext = failed[i].Context;

                    aggregatedMessage += $"{Environment.NewLine}SingleAssert context:{Environment.NewLine}{singleAssertContext}{Environment.NewLine}";
                }
            }

            Assert.AreEqual(0, failed.Count, aggregatedMessage);
        }

        public void AddContext(params string[] context)
        {
            _generalContext.AddRange(context);
        }

        private class SingleAssert
        {
            private readonly string _message;
            private readonly string _expected;
            private readonly string _actual;

            public string Context { get; }
            public bool Failed { get; }

            public SingleAssert(string message, string expected, string actual)
            {
                _message = message;
                _expected = expected;
                _actual = actual;

                Failed = _expected != _actual;
            }

            public SingleAssert(string message, Func<(bool, string, string)> assertion)
            {
                var result = assertion.Invoke();
                _message = message + Environment.NewLine + result.Item2;

                Context = result.Item3;
                Failed = result.Item1;
            }

            public override string ToString()
            {
                if (_expected == null && _actual == null)
                {
                    return _message;
                }

                return $"'{_message}'{Environment.NewLine}assert was expected to be" +
                       $"{Environment.NewLine}'{_expected}'{Environment.NewLine}but was{Environment.NewLine}'{_actual}'";
            }
        }
    }
}