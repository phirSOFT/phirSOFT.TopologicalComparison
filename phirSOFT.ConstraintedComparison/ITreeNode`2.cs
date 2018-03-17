using System.Collections.Generic;

namespace phirSOFT.TopologicalComparison
{
    /// <summary>
    ///     Provides an interface for a node in an <see cref="ITree{T}"/>
    /// </summary>
    /// <typeparam name="TValue">The type of the value stored in the node. </typeparam>
    /// <typeparam name="TNode">The type of the nodes.</typeparam>
    public interface ITreeNode<TValue, TNode> : ITreeNode<TValue> where TNode : ITreeNode<TValue, TNode>
    {
        /// <summary>
        ///     Gets or sets value of the node.
        /// </summary>
        new TValue Value { get; set; }

        /// <summary>
        ///     Gets all children of this node.
        /// </summary>
        new IEnumerable<TNode> Children { get; }

        /// <summary>
        ///     Adds a child to this node.
        /// </summary>
        /// <param name="node">The value of the node to attach</param>
        /// <returns>The new added <see cref="ITreeNode{T}"/>.</returns>
        new TNode AddChild(TValue node);

        /// <summary>
        ///     Detaches an <see cref="ITreeNode{T}"/> from this node.
        /// </summary>
        /// <param name="child"> The node to detach </param>
        /// <remarks>
        ///     The child node has to be an direct child of this node.
        /// </remarks>
        void Detach(TNode child);

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
        void Attach(TNode child);
    }
}
