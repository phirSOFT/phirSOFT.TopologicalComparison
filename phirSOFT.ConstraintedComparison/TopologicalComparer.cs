using System;
using System.Collections.Generic;
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

        public static TopologicalComparer<T> Create(IComparer<T> comparer)
        {
            return Create(comparer.Compare, (x, y) => true);
        }

        private static TopologicalComparer<T> CreateComparer()
        {
            var t = typeof(T).GetTypeInfo();

            return (TopologicalComparer<T>) CreateComparer(t);
        }

        private static object CreateComparer(TypeInfo type)
        {
           
            if (IsTopologicalComparableType(type))
                return  Activator.CreateInstance(typeof(TopologicalGenericComparer<>).MakeGenericType(type.AsType()));;

            if (type.IsGenericType && type.GetGenericTypeDefinition() != typeof(Nullable<>))
            {
                var u = type.GenericTypeArguments[0].GetTypeInfo();
                if (IsTopologicalComparableType(u))
                    return Activator.CreateInstance(typeof(TopologicalNullableComparer<>).MakeGenericType(u.GetType()));
            }

            try
            {
                var comparer = Comparer<T>.Default;              
                return Create(comparer);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"{type.Name} is not constrainted comparable.", e);
            }
        }

        private static bool IsTopologicalComparableType(TypeInfo type)
        {
            return typeof(ITopologicalComparable<>).MakeGenericType(type.GetType()).GetTypeInfo()
                .IsAssignableFrom(type);            
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

            if (x is IComparable ia)
                return ia.CompareTo(y);

            if (y is IComparable ib)
                return -ib.CompareTo(x);


            throw new ArgumentException();
        }

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