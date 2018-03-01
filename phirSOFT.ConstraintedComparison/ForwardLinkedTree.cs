using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace phirSOFT.TopologicalComparison
{

    /// <summary>
    ///     Implements the <see cref="ITree{T}"/> interface in a tree, where
    ///     each node knows there children.
    /// </summary>
    public class ForwardLinkedTree<T> : ITree<T>
    {
        /// <summary>
        ///     Creates a new <see cref="ForwardLinkedTree{T}"/> with a default
        ///     comparer
        /// </summary>
        public ForwardLinkedTree() : this(default(T))
        {
        }

        /// <summary>
        ///     Creates a new <see cref="ForwardLinkedTree{T}"/> with a default
        ///     comparer.
        /// </summary>
        /// <param name="rootValue">The value to assign to the root node</param>
        public ForwardLinkedTree(T rootValue)
        {
            Root = new ForwardLinkedTreeNode(rootValue);
        }

        /// <summary>
        ///     Gets the root node of the tree.
        /// </summary>
        public ITreeNode<T> Root { get; }

        [DebuggerDisplay("Value = {Value}, Children ={_children.Count}")]
        private class ForwardLinkedTreeNode : ITreeNode<T>
        {
            private readonly Collection<ForwardLinkedTreeNode> _children = new Collection<ForwardLinkedTreeNode>();

            public ForwardLinkedTreeNode(T value)
            {
                Value = value;
            }

            public T Value { get; set; }

            public IEnumerable<ITreeNode<T>> Children => new ReadOnlyCollection<ForwardLinkedTreeNode>(_children);

            public ITreeNode<T> AddChild(T node)
            {
                var item = new ForwardLinkedTreeNode(node);
                _children.Add(item);
                return item;
            }

            public void Detach(ITreeNode<T> child)
            {
                _children.Remove(child as ForwardLinkedTreeNode ?? throw new ArgumentException());
            }

            public void Attach(ITreeNode<T> child)
            {
                _children.Add(child as ForwardLinkedTreeNode ?? throw new ArgumentException());
            }
        }
    }
}
