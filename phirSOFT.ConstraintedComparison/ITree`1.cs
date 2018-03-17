namespace phirSOFT.TopologicalComparison
{
    /// <summary>
    ///     Provides a tree data structure
    /// </summary>
    public interface ITree<T>
    {
        /// <summary>
        ///     Returns the root node of the tree.
        /// </summary>
        ITreeNode<T> Root { get; }
    }
}
