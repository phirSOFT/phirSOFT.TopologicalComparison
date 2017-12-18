using System.Collections.Generic;

namespace phirSOFT.ConstraintedComparison
{
    /// <summary>
    /// Abstracts the concept of an <see cref="IComparer{T}"/> in a way, that a comparer is allowed to fail to compare two instances.
    /// </summary>
    /// <typeparam name="T">The type to compare</typeparam>
    public interface IConstraintedComparer<T> : IComparer<T>
    {
        bool CanCompare(T x, T y);
    }
}