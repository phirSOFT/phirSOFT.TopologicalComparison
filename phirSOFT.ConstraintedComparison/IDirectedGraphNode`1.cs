using System.Collections.Generic;

namespace phirSOFT.TopologicalComparison
{
    public interface IDirectedGraphNode<T> : ITreeNode<T, IDirectedGraphNode<T>>
    {
        IEnumerable<IDirectedGraphNode<T>> Parents { get; }
    }
}