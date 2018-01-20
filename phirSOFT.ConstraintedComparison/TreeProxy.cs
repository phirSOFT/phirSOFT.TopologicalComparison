using System;
using System.Collections.Generic;
using System.Linq;

namespace phirSOFT.TopologicalComparison
{
    public static class TreeProxy
    {
        public static void Insert<T>(this ITree<T> tree, T node)
        {
            tree.Insert(node, TopologicalComparer<T>.Default);
        }

        public static void Insert<T>(this ITree<T> tree, T node, ITopologicalComparer<T> comparer)
        {
            var currentNode = tree.Root;
            var
                result = 0; // we have to initialize this here, because we can't convice the compiler result is assigned in the lambda below

            while (currentNode.Children.Any())
                try
                {
                    currentNode = currentNode.Children.First(child =>
                        comparer.TryCompare(currentNode.Value, node, out result) && result <= 0);

                    // The node is allready part of the tree
                    if (result == 0)
                        return;
                }
                catch (InvalidOperationException)
                {
                    // We are here, because we are at the end of the chain
                    break;
                }

            currentNode.Insert(node, child => comparer.TryCompare(currentNode.Value, node, out var r) && r > 0);
        }


        public static void Insert<T>(this IInverseTree<T> tree, T node)
        {
            tree.Insert(node, TopologicalComparer<T>.Default);
        }

        public static void Insert<T>(this IInverseTree<T> tree, T node, ITopologicalComparer<T> comparer)
        {
            bool IsSucessor(IInverseTreeNode<T> possibleSuccessor)
            {
                return comparer.TryCompare(node, possibleSuccessor.Value, out var result) && result <= 0;
            }

            var successorStream = new Queue<IInverseTreeNode<T>>(tree.Leaves.Where(IsSucessor));

            var successors = new List<IInverseTreeNode<T>>();

            while (successorStream.Count > 0)
            {
                var successor = successorStream.Dequeue();
                if (IsSucessor(successor.Parent))
                    successorStream.Enqueue(successor.Parent);
                else
                    successors.Add(successor);
            }

            var first = successors.First();
            var tail = successors.Skip(1).ToList();
            var parent = first.Parent;

            foreach (var successor in tail)
                if (successor.Parent != parent)
                    throw new InvalidTreeStateException();

            var newNode = first.InserBefore(node);

            foreach (var successor in tail) successor.MoveBehind(newNode);
        }
    }
}