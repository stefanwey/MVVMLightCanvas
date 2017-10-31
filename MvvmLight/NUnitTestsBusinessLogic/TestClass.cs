using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLogic.ViewModel;

namespace NUnitTestsBusinessLogic
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here
            Assert.Pass("Your first passing test");
        }

        [Test]
        public void TestDummyCheckPass()
        {
            BeetliData beetli = new BeetliData();

            Assert.AreEqual(true, beetli.DummyCheck());
        }

        [Test]
        public void TestDummyCheckFail()
        {
            BeetliData beetli = new BeetliData();

            Assert.AreEqual(false, beetli.DummyCheck());
        }
    }
}
