using System.Collections.Generic;

namespace phirSOFT.TopologicalComparison
{
    /// <summary>
    ///     Provides an interface for a node in an <see cref="ITree{T}"/>
    /// </summary>
    /// <typeparam name="T">The type of the value stored in the node. </typeparam>
    public interface ITreeNode<T>
    {
        /// <summary>
        ///     Gets or sets value of the node.
        /// </summary>
        T Value { get; set; }

        /// <summary>
        ///     Gets all children of this node.
        /// </summary>
        IEnumerable<ITreeNode<T>> Children { get; }

        /// <summary>
        ///     Adds a child to this node.
        /// </summary>
        /// <param name="node">The value of the node to attach</param>
        /// <returns>The new added <see cref="ITreeNode{T}"/>.<returns>
        ITreeNode<T> AddChild(T node);

        /// <summary>
        ///     Detaches an <see cref="ITreeNode{T}"/> from this node.
        /// </summary>
        /// <param name="child"/> The node to detach </param>
        /// <remarks>
        ///     The child node has to be an direct child of this node.
        /// </remarks>
        void Detach(ITreeNode<T> child);

        /// <summary>
        ///     Attaches an <see cref="ITreeNode{T}"/> to this node.
        /// </summary>
        /// <param name="child">
        ///     The <see cref="ITreeNode{T}"/> to attach.
        /// </param>
        /// <remarks>
        ///     The <paramref name="child"/> should not be attached to
        ///     an other parent.
        /// </remarks>
        void Attach(ITreeNode<T> child);
    }
}
