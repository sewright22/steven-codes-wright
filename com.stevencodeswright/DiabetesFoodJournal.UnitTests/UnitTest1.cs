using AutoFixture;
using DiabetesFoodJournal.DataServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiabetesFoodJournal.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var fixture = new Fixture();
            var ds = fixture.Build<MockAppDataService>().Create();
            Assert.IsTrue(false);
        }
    }
}
