namespace phirSOFT.TopologicalComparison
{
    /// <summary>
    /// Provides a tree datastructure with a more specific node type.
    /// </summary>
    /// <typeparam name="TValue">The type of the values stored in the nodes.</typeparam>
    /// <typeparam name="TNode">The type of the nodes</typeparam>
    public interface ITree<TValue, out TNode> : ITree<TValue> where TNode : ITreeNode<TValue, TNode>
    {
        /// <inheritdoc cref="ITree{T}.Root"/>
        new TNode Root { get; }
    }

}
