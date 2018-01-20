using System.Collections;
using System.Collections.Generic;

namespace phirSOFT.TopologicalComparison
{

    public interface ITree<T>
    {
        ITreeNode<T> Root { get; }
    }

    public interface IInverseTree<T>
    {
        IReadOnlyCollection<IInverseTreeNode<T>> Leaves { get; }
    }

    public interface ITreeNode<T>
    {
        T Value { get; set; }
        IEnumerable<ITreeNode<T>> Children { get; }
    }

    public interface IInverseTreeNode<T>
    {
        T Value { get; set; }
        IInverseTreeNode<T> Parent { get; }
    }
}