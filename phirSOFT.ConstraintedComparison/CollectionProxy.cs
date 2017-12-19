using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace phirSOFT.ConstraintedComparison
{
    public static class CollectionProxy
    {
        public static void Insert<T>(this IList<T> list, T item)
        {
            Insert(list, item, TopologicalComparer<T>.Default);
        }

        public static void Insert<T>(this IList<T> list, T item, ITopologicalComparer<T> comparer)
        {
         
            var index = list.TakeWhile(currentItem => !comparer.CanCompare(currentItem, item) || comparer.Compare(currentItem, item) <= 0).Count();

            list.Insert(index, item);
          
            for (var i = index+2; i < list.Count; i++)
            {
                var x = list[i];
                if (!comparer.CanCompare(x, item) || comparer.Compare(x, item) >= 0) continue;

                list.RemoveAt(i);
                list.Insert(index, x);
            }
        }
    }
}
