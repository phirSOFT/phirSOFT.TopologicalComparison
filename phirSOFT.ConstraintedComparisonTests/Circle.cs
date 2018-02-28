using phirSOFT.TopologicalComparison;

namespace phirSOFT.ConstraintedComparisonTests
{
    public partial class UnitTest1
    {
        internal class Circle : ITopologicalComparable<Circle>
        {
            public readonly int m;
            private readonly int mod;

            public Circle(int m, int mod)
            {
                this.m = m;
                this.mod = mod;
            }

            public int CompareTo(Circle other)
            {
                var tmp = m - other.m;
                if (tmp < 0)
                    tmp += mod;

                if (tmp > mod / 2)
                    tmp = -(mod - tmp);

                return tmp;
            }

            public bool CanCompareTo(Circle other)
            {
                return mod == other.mod;
            }

            public override string ToString()
            {
                return $"{m} mod {mod}";
            }
        }

    }
}
