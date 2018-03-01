using System;
using System.Collections.Generic;

namespace phirSOFT.TopologicalComparison
{
    /// <summary>
    ///     Provides extension methods for the <see cref="ITree{T}"/>
    /// interface.
    /// </summary>
    public static class TreeProxy
    {
        /// <summary>
        ///     Inserts a node into an topological ordered tree. Using the
        ///     default comparer.
        /// </summary>
        /// <param name="tree">
        ///     The tree to insert the <paramref name="node"/> into.
        /// </param>
        /// <param name="node">
        ///     The node to insert into the tree.
        /// </param>
        public static void Insert<T>(this ITree<T> tree, T node)
        {
            tree.Insert(node, TopologicalComparer<T>.Default);
        }

        /// <summary>
        ///     Inserts a node into an topological ordered tree. Using a
        ///     custom comparer.
        /// </summary>
        /// <param name="tree">
        ///     The tree to insert the <paramref name="node"/> into.
        /// </param>
        /// <param name="node">
        ///     The node to insert into the tree.
        /// </param>
        /// <param name="comparer">
        ///     The comparer to use.
        /// </param>
        public static void Insert<T>(this ITree<T> tree, T node, ITopologicalComparer<T> comparer)
        {
            var lastParent = tree.Root ?? throw new ArgumentException($"{nameof(tree)} needs a root.");
            if (comparer.TryCompare(lastParent.Value, node, out var rootres) && rootres == 0)
                return;

            var possibleParents = new Queue<ITreeNode<T>>();
            possibleParents.Enqueue(lastParent);
            var futureChildren = new LinkedList<ITreeNode<T>>();


            while (possibleParents.Count > 0)
            {
                var currentParent = possibleParents.Dequeue();
                var begin = futureChildren.Last;
                var foundLessElment = false;
                foreach (var child in currentParent.Children)
                {
                    if (!comparer.TryCompare(child.Value, node, out var result))
                    {
                        if (!foundLessElment)
                            possibleParents.Enqueue(child);
                        continue;
                    }

                    if (result == 0)
                        return;
                    if (result < 0)
                    {
                        possibleParents.Clear();
                        possibleParents.Enqueue(child);
                        lastParent = child;
                        foundLessElment = true;
                    }
                    else if (result > 0)
                    {
                        futureChildren.AddLast(child);
                    }
                }

                begin = begin?.Next ?? futureChildren.First;

                while (begin != null)
                {
                    currentParent.Detach(begin.Value);
                    begin = begin.Next;
                }
            }


            var newNode = lastParent.AddChild(node);

            foreach (var futureChild in futureChildren) newNode.Attach(futureChild);
        }
    }
}
