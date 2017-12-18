using System;

namespace phirSOFT.ConstraintedComparison
{
    internal class TopologicalComparisonComparer<T> : TopologicalComparer<T>
    {
        private readonly Comparison<T> _comparison;
        private readonly Func<T, T, bool> _canCompare;

        public TopologicalComparisonComparer(Comparison<T> comparison)
        {
            _comparison = comparison;
            _canCompare = (_, __) => true;
        }

        public TopologicalComparisonComparer(Comparison<T> comparison, Func<T, T, bool> canCompare)
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