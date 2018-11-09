using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityServer4.Admin.UITests
{
    [TestClass]
    public class UnitTest1
    {
        [DataTestMethod]
        [DataRow("Client1")]
        [DataRow("Client2")]
        public void TestMethod1(string clientId)
        {
        }
    }
}
