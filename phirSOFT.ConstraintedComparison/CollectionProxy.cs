using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace phirSOFT.TopologicalComparison
{
    public static class CollectionProxy
    {
        public static void Insert<T>(this IList<T> list, T item)
        {
            Insert(list, item, TopologicalComparer<T>.Default);
        }

        public static void InsertUnique<T>(this IList<T> list, T item, Func<T, T, T> resolveConflicts)
        {
            list.InsertUnique(item, TopologicalComparer<T>.Default, resolveConflicts);
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

        public static void InsertUnique<T>(this IList<T> list, T item, ITopologicalComparer<T> comparer, Func<T, T, T> resolveConflicts)
        {
            var result = 0;
            var index = list.TakeWhile(currentItem => !comparer.TryCompare(currentItem, item, out result) || result <= 0).Count();

            int offset;
            if (result < 0)
            {
                list.Insert(index, item);
                offset = 2;
            }
            else
            {
                list[index] = resolveConflicts(list[index], item);
                offset = 1;
            }


            for (var i = index + offset; i < list.Count; i++)
            {
                var x = list[i];
                if (!comparer.TryCompare(x, item, out result))
                    continue;

                
                switch (Math.Sign(result))
                {
                    case -1:
                        list.RemoveAt(i);
                        list.Insert(index++, x);
                        break;
                    case 0:
                        list[index] = resolveConflicts(list[index], x);
                        list.RemoveAt(i);
                        break;
                    case 1:
                        continue;
                }
               

                
            }
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