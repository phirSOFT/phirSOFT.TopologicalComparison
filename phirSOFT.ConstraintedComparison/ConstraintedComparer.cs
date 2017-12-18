using System;
using System.Collections.Generic;
using System.Reflection;

namespace phirSOFT.ConstraintedComparison
{
    public abstract class ConstraintedComparer<T> : IConstraintedComparer<T>
    {
        private static volatile ConstraintedComparer<T> _defaultComparer;

        public static ConstraintedComparer<T> Default
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

        public static ConstraintedComparer<T> Create(Comparison<T> comparison)
        {
            return new ConstraintedComparisonComparer<T>(comparison);
        }

        public static ConstraintedComparer<T> Create(Comparison<T> comparison, Func<T, T, bool> canCompare)
        {
            return new ConstraintedComparisonComparer<T>(comparison, canCompare);
        }

        private static ConstraintedComparer<T> CreateComparer()
        {
            var t = typeof(T).GetTypeInfo();


            if (typeof(IConstraintedComparable<T>).GetTypeInfo().IsAssignableFrom(t))
            {
                return (ConstraintedComparer<T>)Activator.CreateInstance(typeof(ConstraintedGenericComparer<>).MakeGenericType(t.AsType()));
            }

            // If T is a Nullable<U> where U implements IComparable<U> return a NullableComparer<U>
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var u = t.GenericTypeArguments[0].GetTypeInfo();
                if (typeof(IConstraintedComparable<>).MakeGenericType(u.AsType()).GetTypeInfo().IsAssignableFrom(u))
                {
                    return (ConstraintedComparer<T>)Activator.CreateInstance(typeof(ConstraintedGenericComparer<>).MakeGenericType(t.AsType()));
                }
            }
            
            throw new ArgumentException($"{t.Name} is not constrainted comparable.");
        }

        public abstract int Compare(T x, T y);
        public abstract bool CanCompare(T x, T y);
    }

    internal class ConstraintedGenericComparer<T> : ConstraintedComparer<T> where T : IConstraintedComparable<T>
    {
        public override int Compare(T x, T y)
        {
            if (x?.CanCompareTo(y) ?? false)
                return x.CompareTo(y);

            if (y?.CanCompareTo(x) ?? false)
                return y.CompareTo(x);

            return 0;
        }

        public override bool CanCompare(T x, T y)
        {
            return (x?.CanCompareTo(y) ?? false) || (y?.CanCompareTo(x) ?? false);
        }

        // Equals method for the comparer itself. 
        public override bool Equals(object obj)
        {
            return obj is ConstraintedGenericComparer<T>;
        }

        public override int GetHashCode()
        {
            return GetType().Name.GetHashCode();
        }
    }

    internal class ConstraintedNullableComparer<T> : ConstraintedComparer<T?> where T : struct, IConstraintedComparable<T>
    {
        public override int Compare(T? x, T? y)
        {
            if (!(x.HasValue && y.HasValue))
                return 0;


            return Compare((T)x,(T)y);
        }

        private static int Compare(T x, T y)
        {
            if (x.CanCompareTo(y))
                return x.CompareTo(y);

            return y.CanCompareTo(x) ? y.CompareTo(x) : 0;
        }

        public override bool CanCompare(T? x, T? y)
        {
            if (!(x.HasValue && y.HasValue))
                return false;

            return x.Value.CanCompareTo(y.Value) || (y.Value.CanCompareTo(x.Value));
        }

        // Equals method for the comparer itself. 
        public override bool Equals(object obj)
        {
            return obj is ConstraintedGenericComparer<T>;
        }

        public override int GetHashCode()
        {
            return GetType().Name.GetHashCode();
        }
    }

    internal class ConstraintedComparisonComparer<T> : ConstraintedComparer<T>
    {
        private readonly Comparison<T> _comparison;
        private readonly Func<T, T, bool> _canCompare;

        public ConstraintedComparisonComparer(Comparison<T> comparison)
        {
            _comparison = comparison;
            _canCompare = (_, __) => true;
        }

        public ConstraintedComparisonComparer(Comparison<T> comparison, Func<T, T, bool> canCompare)
        {
            _comparison = comparison;
            _canCompare = canCompare;
        }

        public override int Compare(T x, T y)
        {
            return _comparison(x, y);
        }

        public override bool CanCompare(T x, T y)
        {
            return _canCompare(x, y);
        }
    }
}