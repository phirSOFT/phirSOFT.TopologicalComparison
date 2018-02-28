using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phirSOFT.ConstraintedComparisonTests
{
    internal static class RandomEnumerator
    {
        public static IEnumerable<T> PickRandom<T>(IEnumerable<T> input, int seed )
        {
            var data = input.ToList();
            var rnd = new Random(seed);

            while (data.Count > 0)
            {
                var index = rnd.Next(data.Count);
                yield return data[index];
                data.RemoveAt(index);
            }
        }
    }
}
