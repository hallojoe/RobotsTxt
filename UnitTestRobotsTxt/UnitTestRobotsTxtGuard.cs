using HalloJoe.RobotsTxt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestRobotsTxt
{
    /// <summary>
    /// This guy tests the built in guard in RobotsTxt
    /// Test are base on this: https://developers.google.com/search/reference/robots_txt#example-path-matches
    /// </summary>
    [TestClass]
    public class UnitTestRobotsTxtGuard
    {

        [TestMethod]
        public void Test_Fish__Fish_Disallowed()
        {
            var o = new TestCase("/fish", "/fish");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish__Fish_Dot_Html_Disallowed()
        {
            var o = new TestCase("/fish", "/fish.html");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish__Fish_Dash_Salmon_Dot_Html_Disallowed()
        {
            var o = new TestCase("/fish", "/fish/salmon.html");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish__Fishheads_Disallowed()
        {
            var o = new TestCase("/fish", "/fishheads");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish__Fishheads_Dash_Yummy_Dot_Html_Disallowed()
        {
            var o = new TestCase("/fish", "/fishheads/yummy.html");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish__Fish_Dot_Php_Query_Disallowed()
        {
            var o = new TestCase("/fish", "/fish.php?id=anything");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish__Fish_Dot_Asp_Allowed()
        {
            var o = new TestCase("/fish", "/Fish.asp");
            Assert.IsTrue(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish__Catfish_Allowed()
        {
            var o = new TestCase("/fish", "/catfish");
            Assert.IsTrue(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish__Query_Allowed()
        {
            var o = new TestCase("/fish", "/?id=fish");
            Assert.IsTrue(o.Decision.Allowed);
        }






        [TestMethod]
        public void Test_Fish_Asterisk__Fish_Disallowed()
        {
            var o = new TestCase("/fish", "/fish");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish_Asterisk__Fish_Dot_Html_Disallowed()
        {
            var o = new TestCase("/fish", "/fish.html");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish_Asterisk__Fish_Dash_Salmon_Dot_Html_Disallowed()
        {
            var o = new TestCase("/fish*", "/fish/salmon.html");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish_Asterisk__Fishheads_Disallowed()
        {
            var o = new TestCase("/fish*", "/fishheads");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish_Asterisk__Fishheads_Dash_Yummy_Dot_Html_Disallowed()
        {
            var o = new TestCase("/fish*", "/fishheads/yummy.html");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish_Asterisk__Fish_Dot_Php_Query_Disallowed()
        {
            var o = new TestCase("/fish*", "/fish.php?id=anything");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish_Asterisk__Fish_Dot_Asp_Allowed()
        {
            var o = new TestCase("/fish*", "/Fish.asp");
            Assert.IsTrue(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish_Asterisk__Catfish_Allowed()
        {
            var o = new TestCase("/fish*", "/catfish");
            Assert.IsTrue(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish_Asterisk__Query_Allowed()
        {
            var o = new TestCase("/fish*", "/?id=fish");
            Assert.IsTrue(o.Decision.Allowed);
        }



        [TestMethod]
        public void Test_Fish_Dash__Fish_Dash_Disallowed()
        {
            var o = new TestCase("/fish/", "/fish/");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish_Dash__Fish_Dash_Query_Disallowed()
        {
            var o = new TestCase("/fish/", "/fish/?id=anything");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish_Dash__Fish_Dash_Salmon_Dot_Html_Disallowed()
        {
            var o = new TestCase("/fish/", "/fish/salmon.html");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish_Dash__Fish_Allowed()
        {
            var o = new TestCase("/fish/", "/fish");
            Assert.IsTrue(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish_Dash__Fish_Dot_Html_Allowed()
        {
            var o = new TestCase("/fish/", "/fish.html");
            Assert.IsTrue(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish_Dash__Fish_Dash_Salmon__Dot_Html_Allowed()
        {
            var o = new TestCase("/fish/", "/Fish/Salmon.html");
            Assert.IsTrue(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Asterisk_Dot_Php__Filename_Dot_Php__Disallowed()
        {
            var o = new TestCase("/*.php", "/filename.php");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Asterisk_Dot_Php__Folder_Dash_Filename_Dot_Php__Disallowed()
        {
            var o = new TestCase("/*.php", "/folder/filename.php");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Asterisk_Dot_Php__Folder_Dash_Filename_Dot_Php_Query__Disallowed()
        {
            var o = new TestCase("/*.php", "/folder/filename.php?parameters");
            Assert.IsFalse(o.Decision.Allowed);
        }


        [TestMethod]
        public void Test_Asterisk_Dot_Php__Folder_Dash_Any_Dot_Php_Dot_File_Dot_Html__Disallowed()
        {
            var o = new TestCase("/*.php", "/folder/any.php.file.html");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Asterisk_Dot_Php__Filename_Dot_Php_Dash__Disallowed()
        {
            var o = new TestCase("/*.php", "/filename.php/");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Asterisk_Dot_Php__Root__Allowed()
        {
            var o = new TestCase("/*.php", "/");
            Assert.IsTrue(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Asterisk_Dot_Php__Windows_Dot_PHP__Allowed()
        {
            var o = new TestCase("/*.php", "/windows.PHP");
            Assert.IsTrue(o.Decision.Allowed);
        }



        [TestMethod]
        public void Test_Asterisk_Dot_Php_Dollar__Filename_Dot_Php__Disallowed()
        {
            var o = new TestCase("/*.php$", "/filename.php");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Asterisk_Dot_Php_Dollar__Folder_Dash_Filename_Dot_Php__Disallowed()
        {
            var o = new TestCase("/*.php$", "/folder/filename.php");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Asterisk_Dot_Php_Dollar__Filename_Dot_Php_Query__Allowed()
        {
            var o = new TestCase("/*.php$", "/filename.php?parameters");
            Assert.IsTrue(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Asterisk_Dot_Php_Dollar__Filename_Dot_Php_Dash__Allowed()
        {
            var o = new TestCase("/*.php$", "/filename.php/");
            Assert.IsTrue(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Asterisk_Dot_Php_Dollar__Filename_Dot_Php5__Allowed()
        {
            var o = new TestCase("/*.php$", "/filename.php5");
            Assert.IsTrue(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Asterisk_Dot_Php_Dollar__Windows_Dot_Php__Allowed()
        {
            var o = new TestCase("/*.php$", "/windows.PHP");
            Assert.IsTrue(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish_Asterisk_Dot_Php__Fish_Dot_Php__Disallowed()
        {
            var o = new TestCase("/fish*.php", "/fish.php");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish_Asterisk_Dot_Php__Fishheads_Dash_Catfish_Dot_Php_Query__Disallowed()
        {
            var o = new TestCase("/fish*.php", "/fishheads/catfish.php?parameters");
            Assert.IsFalse(o.Decision.Allowed);
        }

        [TestMethod]
        public void Test_Fish_Asterisk_Dot_Php__Fish_Dot_Php__Allowed()
        {
            var o = new TestCase("/fish*.php", "/fish.PHP");
            Assert.IsTrue(o.Decision.Allowed);
        }



    }
}
