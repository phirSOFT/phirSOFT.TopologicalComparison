using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.ConstraintedComparisonTests
{
    [TestClass]
    public class ListTest
    {
        [TestMethod]
        public void TestStringSort_LinkedList()
        {
            string[] strings = new[] { "w", "z", "x", "y", "c", "a", "b" };

            LinkedList<string> list = new LinkedList<string>();
            foreach (string s in strings)
            {
                list.Insert(s, TopologicalComparer<string>.Create(Comparer<string>.Default));
            }

            Array.Sort(strings);
            CollectionAssert.AreEqual(strings, list);
        }

        [TestMethod]
        public void TestStringSort_IList()
        {
            string[] strings = new[] { "w", "z", "x", "y", "c", "a", "b" };

            List<string> list = new List<string>();
            foreach (string s in strings)
            {
                list.Insert(s, TopologicalComparer<string>.Create(Comparer<string>.Default));
            }

            Array.Sort(strings);
            CollectionAssert.AreEqual(strings, list);
        }
    }
}
