using System;

namespace phirSOFT.TopologicalComparison
{
    /// <inheritdoc />
    /// <summary>
    ///     An interface for types types that implement <see cref="T:System.IComparable`1" />, but not all instances can
    ///     compare
    ///     against each other.
    /// </summary>
    /// <remarks>
    ///     It is assumed that <b>if</b> one instance can compare against an other and the other can compare against the first,
    ///     that both comparisons will induct the same order.
    /// </remarks>
    public interface ITopologicalComparable<in T> : IComparable<T>
    {
        /// <summary>
        ///     Determines wheter this instance can compare against <paramref name="other" />.
        /// </summary>
        /// <param name="other">The instance to compare against</param>
        /// <returns>True, if the call to <see cref="IComparable{T}.CompareTo(T)" /> is save.</returns>
        bool CanCompareTo(T other);
    }

    public interface ITopologicalComparable : IComparable
    {
        bool CanCompareTo(object other);
    }
}