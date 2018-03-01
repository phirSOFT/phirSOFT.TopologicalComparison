namespace phirSOFT.TopologicalComparison
{
    /// <summary>
    ///     Provides an tree data structure
    /// </summary>
    public interface ITree<T>
    {
        /// <summary>
        ///     Returns the root node of the tree.
        /// </summary>
        ITreeNode<T> Root { get; }
    }
}
