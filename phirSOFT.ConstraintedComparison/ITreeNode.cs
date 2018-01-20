using System;
using System.Collections.Generic;

namespace phirSOFT.TopologicalComparison
{
    public interface ITreeNode<T>
    {
        T Value { get; set; }
        IEnumerable<ITreeNode<T>> Children { get; }
        ITreeNode<T> AddChild(T node);
        ITreeNode<T> Insert(T node, IEnumerable<ITreeNode<T>> sucessors);
        ITreeNode<T> Insert(T node, Predicate<ITreeNode<T>> sucessors);
        ITreeNode<T> MoveChild(ITreeNode<T> child, ITreeNode<T> newParent);
    }
}