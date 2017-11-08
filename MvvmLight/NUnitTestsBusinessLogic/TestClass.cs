using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;
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
        public void TestIsPointInsideTrue()
        {
            BeetliData beetli = new BeetliData();

            beetli.BeetliLeft = 10;
            beetli.BeetliWidth = 10;
            beetli.BeetliTop = 30;
            beetli.BeetliHeight = 20;

            

            Assert.AreEqual(true, beetli.IsInside(new Point(11, 31)));
            Assert.AreEqual(true, beetli.IsInside(new Point(19, 31)));
            Assert.AreEqual(true, beetli.IsInside(new Point(11, 49)));
            Assert.AreEqual(true, beetli.IsInside(new Point(19, 49)));
        }


        [Test]
        public void TestIsPointInsideFalse()
        {
            BeetliData beetli = new BeetliData();

            beetli.BeetliLeft = 10;
            beetli.BeetliWidth = 10;
            beetli.BeetliTop = 30;
            beetli.BeetliHeight = 20;
            
            Assert.AreEqual(false, beetli.IsInside(new Point(10, 30)));
            Assert.AreEqual(false, beetli.IsInside(new Point(20, 30)));
            Assert.AreEqual(false, beetli.IsInside(new Point(10, 50)));
            Assert.AreEqual(false, beetli.IsInside(new Point(20, 50)));
        }


        [Test]
        public void IntersectsWithTheInsideTrue()
        {
            BeetliData beetli = new BeetliData();

            beetli.BeetliLeft = 10;
            beetli.BeetliWidth = 10;
            beetli.BeetliTop = 30;
            beetli.BeetliHeight = 20;

            // Same Size
            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(10, 30), new Point(20, 50)));

            // Total inside
            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(10, 30), new Point(15, 35)));
            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(12, 30), new Point(18, 35)));
            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(15, 30), new Point(20, 35)));
            
            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(10, 38), new Point(15, 42)));
            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(12, 38), new Point(18, 42)));
            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(15, 38), new Point(20, 42)));

            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(10, 45), new Point(15, 50)));
            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(12, 45), new Point(18, 50)));
            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(15, 45), new Point(20, 50)));

            // Overlapping
            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(8, 25), new Point(15, 35)));
            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(12, 25), new Point(18, 35)));
            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(15, 25), new Point(22, 35)));

            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(8, 38), new Point(15, 42)));
            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(15, 38), new Point(22, 42)));

            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(8, 45), new Point(15, 55)));
            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(12, 45), new Point(18, 55)));
            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(15, 45), new Point(22, 55)));

            // Including the beetli absolute
            Assert.AreEqual(true, beetli.IntersectsWithTheInside(new Point(5, 25), new Point(25, 55)));
        }


        [Test]
        public void IntersectsWithTheInsideFalse()
        {
            BeetliData beetli = new BeetliData();

            beetli.BeetliLeft = 10;
            beetli.BeetliWidth = 10;
            beetli.BeetliTop = 30;
            beetli.BeetliHeight = 20;
            
            // with contact at the corner
            Assert.AreEqual(false, beetli.IntersectsWithTheInside(new Point(5, 25), new Point(10, 30)));
            Assert.AreEqual(false, beetli.IntersectsWithTheInside(new Point(20, 25), new Point(25, 30)));
            Assert.AreEqual(false, beetli.IntersectsWithTheInside(new Point(5, 50), new Point(10, 55)));
            Assert.AreEqual(false, beetli.IntersectsWithTheInside(new Point(20, 50), new Point(25, 55)));

            // with contact at the side
            Assert.AreEqual(false, beetli.IntersectsWithTheInside(new Point(5, 35), new Point(10, 45)));
            Assert.AreEqual(false, beetli.IntersectsWithTheInside(new Point(12, 25), new Point(18, 30)));
            Assert.AreEqual(false, beetli.IntersectsWithTheInside(new Point(12, 50), new Point(18, 55)));
            Assert.AreEqual(false, beetli.IntersectsWithTheInside(new Point(20, 35), new Point(25, 45)));

            //no contact
            Assert.AreEqual(false, beetli.IntersectsWithTheInside(new Point(5, 25), new Point(9, 29)));
            Assert.AreEqual(false, beetli.IntersectsWithTheInside(new Point(21, 25), new Point(25, 29)));
            Assert.AreEqual(false, beetli.IntersectsWithTheInside(new Point(5, 51), new Point(9, 55)));
            Assert.AreEqual(false, beetli.IntersectsWithTheInside(new Point(21, 51), new Point(25, 55)));
            
            Assert.AreEqual(false, beetli.IntersectsWithTheInside(new Point(5, 35), new Point(9, 45)));
            Assert.AreEqual(false, beetli.IntersectsWithTheInside(new Point(12, 25), new Point(18, 29)));
            Assert.AreEqual(false, beetli.IntersectsWithTheInside(new Point(12, 51), new Point(18, 55)));
            Assert.AreEqual(false, beetli.IntersectsWithTheInside(new Point(21, 35), new Point(25, 45)));
        }

    }
}
