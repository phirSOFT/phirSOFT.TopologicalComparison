using System.Collections.Generic;

namespace phirSOFT.TopologicalComparison
{
    public interface ITreeNode<T>
    {
        T Value { get; set; }
        IEnumerable<ITreeNode<T>> Children { get; }
        ITreeNode<T> AddChild(T node);
        void Detach(ITreeNode<T> child);
        void Attach(ITreeNode<T> child);
    }
}