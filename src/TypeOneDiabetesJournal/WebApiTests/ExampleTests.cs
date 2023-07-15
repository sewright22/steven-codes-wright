using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Tests
{
    [TestClass()]
    public class ExampleTests
    {
        [TestMethod()]
        public void SaveFileTest()
        {
            var example = new Example();
            example.SaveFile("c:/file.txt", "Save to text");
            Assert.Fail();
        }
    }
}