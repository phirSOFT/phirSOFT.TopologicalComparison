using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace phirSOFT.ConstraintedComparisonTests
{
    [TestClass]
    public class TreeComparerTest
    {
        [TestMethod]
        public void TestTreeComparer()
        {
            var cmp = new TreeComparer();
            Assert.IsFalse(cmp.CanCompare("0000", "0"));
            Assert.IsFalse(cmp.CanCompare("0000", "1"));

            Assert.IsFalse(cmp.CanCompare("0", "0000"));
            Assert.IsFalse(cmp.CanCompare("1", "0000"));
        }

        [TestMethod]
        public void TestEqualComparer()
        {
            var cmp = new TreeComparer();
            Assert.IsTrue(cmp.CanCompare("0000", "0000"));
            Assert.AreEqual(0, cmp.Compare("0000", "0000"));

;
        }
    }
}
