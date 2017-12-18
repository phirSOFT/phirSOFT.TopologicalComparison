namespace phirSOFT.ConstraintedComparison
{
    internal class TopologicalGenericComparer<T> : TopologicalComparer<T> where T : ITopologicalComparable<T>
    {
        public override int Compare(T x, T y)
        {
            if (x?.CanCompareTo(y) ?? false)
                return x.CompareTo(y);

            if (y?.CanCompareTo(x) ?? false)
                return y.CompareTo(x);

            return 0;
        }

        public override bool CanCompare(T x, T y)
        {
            return (x?.CanCompareTo(y) ?? false) || (y?.CanCompareTo(x) ?? false);
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