using System.Collections.Generic;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.ConstraintedComparisonTests
{
    internal class AbsoluteTopological<T> : ITopologicalComparable<AbsoluteTopological<T>>
    {
        public T Value { get; }

        private static readonly Dictionary<T, AbsoluteTopological<T>> Constraints = new Dictionary<T, AbsoluteTopological<T>>();

        private readonly List<AbsoluteTopological<T>> _before = new List<AbsoluteTopological<T>>();

        private readonly List<AbsoluteTopological<T>> _after = new List<AbsoluteTopological<T>>();


        public AbsoluteTopological(T value)
        {
            Value = value;
            Constraints.Add(value, this);
        }


        public void Before(AbsoluteTopological<T> other)
        {
            other._after.Add(this);
            _before.Add(other);
        }

        public void After(AbsoluteTopological<T> other)
        {
            other._before.Add(this);
            _after.Add(other);
        }

        public int CompareTo(AbsoluteTopological<T> other)
        {
            if (_after.Contains(other))
                return 1;
            if (_before.Contains(other))
                return -1;
            return 0;
        }

        public bool CanCompareTo(AbsoluteTopological<T> other)
        {
            return _after.Contains(other) || _before.Contains(other);
        }

        public static implicit operator T(AbsoluteTopological<T> val)
        {
            return val.Value;
        }

        public static implicit operator AbsoluteTopological<T>(T val)
        {
            if (Constraints.ContainsKey(val))
                return Constraints[val];
            var c = new AbsoluteTopological<T>(val);
            return c;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}