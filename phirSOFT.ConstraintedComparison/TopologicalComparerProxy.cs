namespace phirSOFT.TopologicalComparison
{
    public static class TopologicalComparerProxy
    {
        public static bool TryCompare(this ITopologicalComparer comparer, object lhs, object rhs, out int result)
        {
            if (comparer.CanCompare(lhs, rhs))
            {
                result = comparer.Compare(lhs, rhs);
                return true;
            }

            result = 0;
            return false;
        }

        public static bool TryCompare<T>(this ITopologicalComparer<T> comparer, T lhs, T rhs, out int result)
        {
            if (comparer.CanCompare(lhs, rhs))
            {
                result = comparer.Compare(lhs, rhs);
                return true;
            }

            result = 0;
            return false;
        }
    }
}