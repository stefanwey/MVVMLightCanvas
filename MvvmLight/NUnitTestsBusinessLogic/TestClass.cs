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

            

            Assert.AreEqual(true, beetli.Contains(new Point(11, 31)));
            Assert.AreEqual(true, beetli.Contains(new Point(19, 31)));
            Assert.AreEqual(true, beetli.Contains(new Point(11, 49)));
            Assert.AreEqual(true, beetli.Contains(new Point(19, 49)));
        }


        [Test]
        public void TestIsPointInsideFalse()
        {
            BeetliData beetli = new BeetliData();

            beetli.BeetliLeft = 10;
            beetli.BeetliWidth = 10;
            beetli.BeetliTop = 30;
            beetli.BeetliHeight = 20;
            
            Assert.AreEqual(false, beetli.Contains(new Point(10, 30)));
            Assert.AreEqual(false, beetli.Contains(new Point(20, 30)));
            Assert.AreEqual(false, beetli.Contains(new Point(10, 50)));
            Assert.AreEqual(false, beetli.Contains(new Point(20, 50)));
        }


        [Test]
        public void TestIsOverlappingTrue()
        {
            BeetliData beetli = new BeetliData();

            beetli.BeetliLeft = 10;
            beetli.BeetliWidth = 10;
            beetli.BeetliTop = 30;
            beetli.BeetliHeight = 20;

            // Same Size
            Assert.AreEqual(true, beetli.IsOverlapping(10, 30, 20, 50));

            // Total inside
            Assert.AreEqual(true, beetli.IsOverlapping(10, 30, 15, 35));
            Assert.AreEqual(true, beetli.IsOverlapping(12, 30, 18, 35));
            Assert.AreEqual(true, beetli.IsOverlapping(15, 30, 20, 35));
            
            Assert.AreEqual(true, beetli.IsOverlapping(10, 38, 15, 42));
            Assert.AreEqual(true, beetli.IsOverlapping(12, 38, 18, 42));
            Assert.AreEqual(true, beetli.IsOverlapping(15, 38, 20, 42));

            Assert.AreEqual(true, beetli.IsOverlapping(10, 45, 15, 50));
            Assert.AreEqual(true, beetli.IsOverlapping(12, 45, 18, 50));
            Assert.AreEqual(true, beetli.IsOverlapping(15, 45, 20, 50));

            // Overlapping
            Assert.AreEqual(true, beetli.IsOverlapping(8, 25, 15, 35));
            Assert.AreEqual(true, beetli.IsOverlapping(12, 25, 18, 35));
            Assert.AreEqual(true, beetli.IsOverlapping(15, 25, 22, 35));

            Assert.AreEqual(true, beetli.IsOverlapping(8, 38, 15, 42));
            Assert.AreEqual(true, beetli.IsOverlapping(15, 38, 22, 42));

            Assert.AreEqual(true, beetli.IsOverlapping(8, 45, 15, 55));
            Assert.AreEqual(true, beetli.IsOverlapping(12, 45, 18, 55));
            Assert.AreEqual(true, beetli.IsOverlapping(15, 45, 22, 55));

            // Including the beetli absolute
            Assert.AreEqual(true, beetli.IsOverlapping(5, 25, 25, 55));
        }


        [Test]
        public void TestIsOverlappingFalse()
        {
            BeetliData beetli = new BeetliData();

            beetli.BeetliLeft = 10;
            beetli.BeetliWidth = 10;
            beetli.BeetliTop = 30;
            beetli.BeetliHeight = 20;
            
            // with contact at the corner
            Assert.AreEqual(false, beetli.IsOverlapping(5, 25, 10, 30));
            Assert.AreEqual(false, beetli.IsOverlapping(20, 25, 25, 30));
            Assert.AreEqual(false, beetli.IsOverlapping(5, 50, 10, 55));
            Assert.AreEqual(false, beetli.IsOverlapping(20, 50, 25, 55));

            // with contact at the side
            Assert.AreEqual(false, beetli.IsOverlapping(5, 35, 10, 45));
            Assert.AreEqual(false, beetli.IsOverlapping(12, 25, 18, 30));
            Assert.AreEqual(false, beetli.IsOverlapping(12, 50, 18, 55));
            Assert.AreEqual(false, beetli.IsOverlapping(20, 35, 25, 45));

            //no contact
            Assert.AreEqual(false, beetli.IsOverlapping(5, 25, 9, 29));
            Assert.AreEqual(false, beetli.IsOverlapping(21, 25, 25, 29));
            Assert.AreEqual(false, beetli.IsOverlapping(5, 51, 9, 55));
            Assert.AreEqual(false, beetli.IsOverlapping(21, 51, 25, 55));
            
            Assert.AreEqual(false, beetli.IsOverlapping(5, 35, 9, 45));
            Assert.AreEqual(false, beetli.IsOverlapping(12, 25, 18, 29));
            Assert.AreEqual(false, beetli.IsOverlapping(12, 51, 18, 55));
            Assert.AreEqual(false, beetli.IsOverlapping(21, 35, 25, 45));
        }

    }
}
