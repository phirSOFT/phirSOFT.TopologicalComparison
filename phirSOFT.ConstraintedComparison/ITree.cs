namespace phirSOFT.TopologicalComparison
{
    public interface ITree<T>
    {
        ITreeNode<T> Root { get; }
    }
}