﻿using System.Collections.Generic;

namespace phirSOFT.ConstraintedComparison
{
    /// <inheritdoc />
    /// <summary>
    /// Abstracts the concept of an <see cref="T:System.Collections.Generic.IComparer`1" /> in a way, that a comparer is allowed to fail to compare two instances.
    /// </summary>
    /// <typeparam name="T">The type to compare</typeparam>
    public interface ITopologicalComparer<in T> : IComparer<T>
    {
        bool CanCompare(T x, T y);
    }
}