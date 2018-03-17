using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.ConstraintedComparisonTests
{
    [TestClass]
    public class TypeComparerUnitTest
    {
        [TestMethod]
        public void TestRoot()
        {
            var cmp = new TypeComparer();
            Assert.IsTrue(cmp.CanCompare(typeof(object), typeof(TypeComparer)));
            Assert.IsTrue(cmp.Compare(typeof(object), typeof(TypeComparer)) < 0);
        }

        [TestMethod]
        public void TestInterfaceOnClass()
        {
            var cmp = new TypeComparer();
            Assert.IsTrue(cmp.CanCompare(typeof(ITopologicalComparer), typeof(TypeComparer)));
            Assert.IsTrue(cmp.Compare(typeof(ITopologicalComparer), typeof(TypeComparer)) < 0);
        }
    }
}
