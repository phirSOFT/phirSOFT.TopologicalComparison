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
    public class TopologicalComparerTest
    {
        [TestMethod]
        public void TestPrimitiveInt()
        {
            Assert.IsNotNull(TopologicalComparer<int>.Default);
        }

        [TestMethod]
        public void TestPrimitiveDouble()
        {
            Assert.IsNotNull(TopologicalComparer<double>.Default);
        }

        [TestMethod]
        public void TestPrimitiveString()
        {
            Assert.IsNotNull(TopologicalComparer<string>.Default);
        }

        [TestMethod]
        public void TestPrimitiveObject_ExpectThrow()
        {
            Assert.IsNotNull(TopologicalComparer<object>.Default);
        }


    }
}
