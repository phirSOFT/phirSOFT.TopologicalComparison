using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace phirSOFT.TopologicalComparison
{
    /// <inheritdoc cref="ITopologicalComparer{T}"/>
    /// <summary>
    /// A topological comparer that wraps multiple <see cref="ITopologicalComparer{T}"/> into one.
    /// </summary>
    /// <typeparam name="T">The type to compare</typeparam>
    /// <remarks>
    /// The comparers are applied in ascending index order.
    /// </remarks>
    public class  AccumulatedTopologicalComparer<T> : Collection<ITopologicalComparer<T>>, ITopologicalComparer<T>
    {
        /// <inheritdoc cref="IComparer{T}.Compare"/>
        public int Compare(T x, T y)
        {
            return this.First(item => item.CanCompare(x, y)).Compare(x, y);
        }

        /// <inheritdoc cref="ITopologicalComparer{T}.CanCompare"/>
        public bool CanCompare(T x, T y)
        {
            return this.Any(item => item.CanCompare(x, y));
        }
    }
}
