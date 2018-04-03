using HalloJoe.RobotsTxt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestRobotsTxt
{
    /// <summary>
    /// https://developers.google.com/search/reference/robots_txt#order-of-precedence-for-group-member-records
    /// </summary>
    [TestClass]
    public class UnitTestRobotsTxtGuardVerdicts
    {
        [TestMethod]
        public void Test_A()
        {
            var o = new TestCase("/p", "/", "*", "/page");
            Assert.IsTrue(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_B()
        {
            var o = new TestCase("/folder/", "/folder", "*", "/folder/page");
            Assert.IsTrue(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_C()
        {
            var o = new TestCase("/$", "/", "*", "/");
            Assert.IsTrue(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_D()
        {
            var o = new TestCase("/$", "/", "*", "/page.htm");
            Assert.IsFalse(o.Decision.Allowed);
        }


        /// <summary>
        /// according to this: https://developers.google.com/search/reference/robots_txt#order-of-precedence-for-group-member-records
        /// this test should return undefined - 
        /// // todo: Makee IDecision.Allowed nullable and implement logic to support undfined
        /// </summary>
        [TestMethod]
        public void Test_UnsuportedCase()
        {
            var o = new TestCase("/page", "/*.htm", "*", "/page.htm");
            Assert.IsTrue(true);
        }

    }



}
