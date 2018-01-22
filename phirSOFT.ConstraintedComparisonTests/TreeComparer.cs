using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.ConstraintedComparisonTests
{
    class TreeComparer : ITopologicalComparer<string>
    {
        public int Compare(string x, string y)
        {
            Debug.Assert(x != null, nameof(x) + " != null");
            Debug.Assert(y != null, nameof(y) + " != null");
            return x.Length - y.Length;
        }

        public bool CanCompare(string x, string y)
        {
            var r = Compare(x, y);
            return (Math.Abs(r) == 1 && (r == -1 ? y.StartsWith(x) : x.StartsWith(y))) || x == y;
        }
    }
}
