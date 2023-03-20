using System.Collections.Generic;

namespace phirSOFT.TopologicalComparison
{
    public class SubstringComparer<T, TItem> : ITopologicalComparer<T> where T : IEnumerable<TItem>
    {
        private readonly IEqualityComparer<TItem> _itemComparer;

        public SubstringComparer() : this(EqualityComparer<TItem>.Default)
        {
        }

        public SubstringComparer(IEqualityComparer<TItem> itemComparer)
        {
            _itemComparer = itemComparer;
        }

        public int Compare(T x, T y)
        {
            TryCompare(x, y, out var result);
            return result;
        }

        public bool CanCompare(T x, T y)
        {
            return TryCompare(x, y, out _);
        }

        private bool TryCompare(T x, T y, out int result)
        {
            if (x == null || y == null)
            {
                result = 0;
                return false;
            }

            using var left = x.GetEnumerator();
            using var right = y.GetEnumerator();

            do
            {
                if (!left.MoveNext())
                {
                    result = right.MoveNext() ? -1 : 0;
                    return true;
                }

                if (!right.MoveNext())
                {
                    result = 1;
                    return true;
                }

                if (!_itemComparer.Equals(left.Current, right.Current))
                {
                    result = 0;
                    return false;
                }
            } while (true);
        }
    }
}