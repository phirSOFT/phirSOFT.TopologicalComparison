using System;
using System.Reflection;

namespace phirSOFT.TopologicalComparison
{
    public abstract class TopologicalComparer<T> : ITopologicalComparer<T>
    {
        private static volatile TopologicalComparer<T> _defaultComparer;

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

        public abstract int Compare(T x, T y);
        public abstract bool CanCompare(T x, T y);

        public static TopologicalComparer<T> Create(Comparison<T> comparison)
        {
            return new TopologicalComparisonComparer<T>(comparison);
        }

        public static TopologicalComparer<T> Create(Comparison<T> comparison, Func<T, T, bool> canCompare)
        {
            return new TopologicalComparisonComparer<T>(comparison, canCompare);
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

            throw new ArgumentException($"{t.Name} is not constrainted comparable.");
        }
    }

    public sealed class TopologicalComparer : ITopologicalComparer
    {
        public static TopologicalComparer Default => new TopologicalComparer();

        public int Compare(object x, object y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            // Todo: Check for ITopologicalComparable<T> here

            if (x is ITopologicalComparable ia)
                return ia.CompareTo(y);

            if (y is ITopologicalComparable ib)
                return -ib.CompareTo(x);

            throw new ArgumentException();
        }

        public bool CanCompare(object x, object y)
        {
            return x == y
                   || x == null
                   || y == null
                   || x is ITopologicalComparable ia && ia.CanCompareTo(y)
                   || y is ITopologicalComparable ib && ib.CanCompareTo(x);
        }
    }
}