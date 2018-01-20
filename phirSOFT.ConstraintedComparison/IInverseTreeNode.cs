namespace phirSOFT.TopologicalComparison
{
    public interface IInverseTreeNode<T>
    {
        T Value { get; set; }
        IInverseTreeNode<T> Parent { get; }
        IInverseTreeNode<T> InserBefore(T node);
        void MoveBehind(IInverseTreeNode<T> node);
    }
}