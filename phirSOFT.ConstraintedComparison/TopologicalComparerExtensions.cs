namespace phirSOFT.TopologicalComparison
{
    /// <summary>
    ///     Provides extension method for the <see cref="ITopologicalComparer"/> and 
    ///     <see cref="ITopologicalComparer{T}"/> interfaces.
    /// </summary>
    public static class TopologicalComparerExtensions
    {
        /// <summary>
        ///     Tries to compare two objects using an <see
        ///     cref="ITopologicalComparer"/>.
        /// </summary>
        /// <param name="comparer"> The comparer to use.</param>
        /// <param name="lhs">The first object to compare.</param>
        /// <param name="rhs">The second object to compare.</param>
        /// <param name="result">
        ///     The result of the comparison (if sucessfull). Otherwise the
        ///     result is set to 0.
        /// </param>
        /// <returns>
        ///     True, if the comparison suecced.
        /// </returns>
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

        /// <summary>
        ///     Tries to compare two objects using an <see
        ///     cref="ITopologicalComparer{T}/">.
        /// </summary>
        /// <param name="comparer"> The comparer to use.</param>
        /// <param name="lhs">The first object to compare.</param>
        /// <param name="rhs">The second object to compare.</param>
        /// <param name="result">
        ///     The result of the comparison (if sucessfull). Otherwise the
        ///     result is set to 0.
        /// </param>
        /// <returns>
        ///     True, if the comparison suecced.
        /// </returns>
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
