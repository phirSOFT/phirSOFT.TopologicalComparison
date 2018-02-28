using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.ConstraintedComparisonTests
{
    class ComplexComparer : ITopologicalComparer<Complex>
    {
        public int Compare(Complex x, Complex y)
        {
            return Math.Sign(x.Magnitude - y.Magnitude);
        }

        public bool CanCompare(Complex x, Complex y)
        {
            if (x == Complex.Zero || y == Complex.Zero)
                return true;

            return (x.Phase - y.Phase) % Math.PI < double.Epsilon;
        }
    }
}
