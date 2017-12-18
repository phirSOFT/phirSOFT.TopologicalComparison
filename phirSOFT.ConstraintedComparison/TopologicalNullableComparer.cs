namespace phirSOFT.ConstraintedComparison
{
    internal class TopologicalNullableComparer<T> : TopologicalComparer<T?> where T : struct, ITopologicalComparable<T>
    {
        public override int Compare(T? x, T? y)
        {
            if (!(x.HasValue && y.HasValue))
                return 0;


            return Compare((T)x,(T)y);
        }

        private static int Compare(T x, T y)
        {
            if (x.CanCompareTo(y))
                return x.CompareTo(y);

            return y.CanCompareTo(x) ? y.CompareTo(x) : 0;
        }

        public override bool CanCompare(T? x, T? y)
        {
            if (!(x.HasValue && y.HasValue))
                return false;

            return x.Value.CanCompareTo(y.Value) || (y.Value.CanCompareTo(x.Value));
        }

        // Equals method for the comparer itself. 
        public override bool Equals(object obj)
        {
            return obj is TopologicalGenericComparer<T>;
        }

        public override int GetHashCode()
        {
            return GetType().Name.GetHashCode();
        }
    }
}