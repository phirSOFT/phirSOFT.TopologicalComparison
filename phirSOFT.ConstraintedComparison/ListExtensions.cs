using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace phirSOFT.TopologicalComparison
{
    public static class ListExtensions
    {
        public static void Insert<T>(this IList<T> list, T item)
        {
            Insert(list, item, TopologicalComparer<T>.Default);
        }

        public static void Insert<T>(this IList<T> list, T item, ITopologicalComparer<T> comparer)
        {
            var index = list.TakeWhile(currentItem =>
                !comparer.CanCompare(currentItem, item) || comparer.Compare(currentItem, item) <= 0).Count();

            list.Insert(index, item);

            for (var i = index + 2; i < list.Count; i++)
            {
                var x = list[i];
                if (!comparer.CanCompare(x, item) || comparer.Compare(x, item) >= 0) continue;

                list.RemoveAt(i);
                list.Insert(index++, x);
            }
        }

        public static void Insert<T>(this LinkedList<T> list, T item)
        {
            return list.Insert(item, TopologicalComparer<T>.Default);
        }

        public static void Insert<T>(this LinkedList<T> list, T item, ITopologicalComparer<T> comparer)
        {
            var currentItem = list.First;

            while (currentItem != null &&
                   (!comparer.TryCompare(currentItem.Value, item, out var result) || result <= 0))
                currentItem = currentItem.Next;


            var inserted = currentItem != null ? list.AddAfter(currentItem, item) : list.AddLast(item);

            for (currentItem = inserted.Next; currentItem != null; currentItem = currentItem?.Next)
            {
                if (!comparer.CanCompare(currentItem.Value, item) ||
                    comparer.Compare(currentItem.Value, item) >= 0) continue;

                var toBeMoved = currentItem;
                currentItem = currentItem.Next;
                list.Remove(toBeMoved);
                list.AddBefore(inserted, toBeMoved);
            }
        }

        public static void Insert(this IList list, object item)
        {
            Insert(list, item, TopologicalComparer.Default);
        }

        public static void Insert(this IList list, object item, ITopologicalComparer comparer)
        {
            var index = 0;
            var enumerator = list.GetEnumerator();
            while (enumerator.MoveNext() && (!comparer.CanCompare(enumerator.Current, item) ||
                                             comparer.Compare(enumerator.Current, item) <= 0))
                index++;


            list.Insert(index, item);

            for (var i = index + 2; i < list.Count; i++)
            {
                var x = list[i];
                if (!comparer.CanCompare(x, item) || comparer.Compare(x, item) >= 0) continue;

                list.RemoveAt(i);
                list.Insert(index++, x);
            }
        }
    }
}
