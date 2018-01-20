using System.Collections.Generic;

namespace phirSOFT.TopologicalComparison
{
    public interface IInverseTree<T>
    {
        IReadOnlyCollection<IInverseTreeNode<T>> Leaves { get; }
    }
}