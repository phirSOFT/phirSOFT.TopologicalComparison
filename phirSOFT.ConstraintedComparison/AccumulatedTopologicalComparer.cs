using System.Collections.ObjectModel;
using System.Linq;


namespace phirSOFT.TopologicalComparison
{
    public class  AccumulatedTopologicalComparer<T> : Collection<ITopologicalComparer<T>>, ITopologicalComparer<T>
    {
        public int Compare(T x, T y)
        {
            return this.First(item => item.CanCompare(x, y)).Compare(x, y);
        }

        public bool CanCompare(T x, T y)
        {
            return this.Any(item => item.CanCompare(x, y));
        }
    }
}
