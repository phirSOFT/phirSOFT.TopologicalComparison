using System.Collections;
using System.Collections.Generic;

namespace phirSOFT.TopologicalComparison
{
    /// <inheritdoc />
    /// <summary>
    ///     Abstracts the concept of an <see cref="T:System.Collections.Generic.IComparer`1" /> in a way, that a comparer is
    ///     allowed to fail to compare two instances.
    /// </summary>
    /// <typeparam name="T">The type to compare</typeparam>
    public interface ITopologicalComparer<in T> : IComparer<T>
    {
        /// <summary>
        ///     Determines wheter <paramref name="x"/> and <paramref name="y"/>
        ///     can be compared.
        /// </summary>
        /// <param name="x"> The first operand to compare.</param>
        /// <param name="y"> The second operand to compare.</param>
        /// <returns> True, if the two objects can be compared. </returns>
        bool CanCompare(T x, T y);
    }
}
