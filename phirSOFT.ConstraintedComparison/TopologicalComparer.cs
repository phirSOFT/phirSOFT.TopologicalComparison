using System;
using System.Collections.Generic;
using System.Reflection;

namespace phirSOFT.TopologicalComparison
{
    /// <inheritdoc/>
    /// <summary>
    ///     Provides a base implementation of the 
    ///     <see cref="ITopologicalComparer"> interface.
    /// </summary>
    /// <typeparam name="T">The type to compare.</typeparam>
    public abstract class TopologicalComparer<T> : ITopologicalComparer<T>
    {
        private static volatile TopologicalComparer<T> _defaultComparer;

        /// <summary>
        ///     Gets the default TopologicalComparer of the given type.
        /// </summary>
        public static TopologicalComparer<T> Default
        {
            get
            {
                var comparer = _defaultComparer;

                if (comparer != null) return comparer;

                comparer = CreateComparer();
                _defaultComparer = comparer;

                return comparer;
            }
        }

        /// <inheritdoc/>
        public abstract int Compare(T x, T y);

        /// <inheritdoc/>
        public abstract bool CanCompare(T x, T y);

        /// <summary>
        ///     Creates a new <see cref="TopologicalComparer{T}"/> from a <see
        ///      cref"Comparison{T}"/>.
        /// </summary>
        /// <param name="comparison">
        ///     The comparison to use for the comparer.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="TopologicalComparer{T}"/> that can compare
        ///     all elements.
        /// </returns>
        /// <remarks>
        ///     The <see cref="TopologicalComparer{T}"/> returned by this
        ///     method, will behave equivalent to the <see cref="Comparer{T}"/>
        ///     returned by <see cref="Comparer{T}.Create(Comparison{T})"/>
        /// </remarks>
        public static TopologicalComparer<T> Create(Comparison<T> comparison)
        {
            return new TopologicalComparisonComparer<T>(comparison);
        }

        /// <summary>
        ///     Creates a new <see cref="TopologicalComparer{T}"/> from a <see
        ///     cref="Comparison{T}"/> and a <see cref="Func{T,T,T}"/> that
        ///     deterimines, wheter two objects can be compared.
        /// </summary>
        /// <param name="comparison">
        ///     The comparsion to use for the comparer.
        /// </param>
        /// <param name="canCompare">
        ///     A function that determines, wether two elements can be
        ///     compared.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="TopologicalComparer{T}"/> that delegates
        ///     the function calls to the <paramref name="comparison"/> and
        ///     <paramref name="canCompare"/>.
        /// </returns>
        public static TopologicalComparer<T> Create(Comparison<T> comparison, Func<T, T, bool> canCompare)
        {
            return new TopologicalComparisonComparer<T>(comparison, canCompare);
        }

        /// <summary>
        ///     Wraps an <see cref="IComparer{T}"/> into an <see
        ///     cref="TopologicalComparer{T}"/>.
        /// </summary>
        /// <param name="comparer"/>
        ///     The <see cref="IComparer{T}"/> to wrap into an <see
        ///     cref="TopologicalComparer{T}"/>
        /// </param>
        /// <remarks>
        ///     The <see cref="TopologicalComparer{T}"/> returned by this
        ///     method, will behave equivalent to the <see cref="IComparer{T}"/>
        ///     specified at <paramref name="comparer"/>.
        /// </remarks>
        public static TopologicalComparer<T> Create(IComparer<T> comparer)
        {
            return Create(comparer.Compare, (x, y) => true);
        }

        private static TopologicalComparer<T> CreateComparer()
        {
            var t = typeof(T).GetTypeInfo();

            if (typeof(ITopologicalComparable<T>).GetTypeInfo().IsAssignableFrom(t))
                return (TopologicalComparer<T>) Activator.CreateInstance(
                    typeof(TopologicalGenericComparer<>).MakeGenericType(t.AsType()));

            // If T is a Nullable<U> where U implements IComparable<U> return a NullableComparer<U>
            if (!t.IsGenericType || t.GetGenericTypeDefinition() != typeof(Nullable<>))
                throw new ArgumentException($"{t.Name} is not constrainted comparable.");
            var u = t.GenericTypeArguments[0].GetTypeInfo();
            if (typeof(ITopologicalComparable<>).MakeGenericType(u.AsType()).GetTypeInfo().IsAssignableFrom(u))
                return (TopologicalComparer<T>) Activator.CreateInstance(
                    typeof(TopologicalGenericComparer<>).MakeGenericType(t.AsType()));

            try
            {
                var comparer = Comparer<T>.Default;
                return Create(comparer);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"{t.Name} is not constrainted comparable.", e);
            }
        }
    }

    /// <summary>
    ///     Provides an implementation of <see cref="ITopologicalComparer"/>
    /// </summary>
    public sealed class TopologicalComparer : ITopologicalComparer
    {
        /// <summary>
        ///     Gets the default <see cref="TopologicalComparer"/>.
        /// </summary>
        public static TopologicalComparer Default => new TopologicalComparer();

        /// <inheritdoc/>
        public int Compare(object x, object y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            // Todo: Check for ITopologicalComparable<T> here

            if (x is IComparable ia)
                return ia.CompareTo(y);

            if (y is IComparable ib)
                return -ib.CompareTo(x);


            throw new ArgumentException();
        }

        /// <inheritdoc/>
        public bool CanCompare(object x, object y)
        {
            return x == y
                   || x == null
                   || y == null
                   || x is ITopologicalComparable ia && ia.CanCompareTo(y)
                   || y is ITopologicalComparable ib && ib.CanCompareTo(x)
                   || x is IComparable
                   || y is IComparable;
        }
    }
}
