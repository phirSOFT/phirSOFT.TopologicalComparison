using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.ConstraintedComparisonTests
{
    [TestClass]
    public class UniqueInsertTest
    {
        [TestMethod]
        public void TestIList()
        {
            var list = new List<Complex>();

            var point = 1 + 2 * Complex.ImaginaryOne;

            var factors = Enumerable.Repeat(new Random(), 20).Select(r => r.NextDouble()).Distinct();
            var rawPoints = new List<Complex>();
            var enumerable = factors as double[] ?? factors.ToArray();
            foreach (var factor in enumerable)
            {
                rawPoints.Add(factor * point);
                if (factor * point != Complex.Zero)
                    rawPoints.Add(-factor * point);
            }

            var comparer = new ComplexComparer();
            foreach (var p in RandomEnumerator.PickRandom(rawPoints, 1))
            {
                list.InsertUnique(p, comparer, (old, @new) => old.Phase < @new.Phase ? old : @new);
            }

            CollectionAssert.AreEquivalent(enumerable.OrderBy(d => d).Select(d => d * point).ToList(), list);
        }
    }
}
