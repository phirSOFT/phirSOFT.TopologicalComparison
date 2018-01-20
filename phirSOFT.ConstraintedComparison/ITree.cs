using System;
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
        ITreeNode<T> AddChild(T node);
        ITreeNode<T> Insert(T node, IEnumerable<ITreeNode<T>> sucessors);
        ITreeNode<T> Insert(T node, Predicate<ITreeNode<T>> sucessors);
        ITreeNode<T> MoveChild(ITreeNode<T> child, ITreeNode<T> newParent);
    }

    public interface IInverseTreeNode<T>
    {
        T Value { get; set; }
        IInverseTreeNode<T> Parent { get; }
        IInverseTreeNode<T> InserBefore(T node);
        void MoveBehind(IInverseTreeNode<T> node);
    }
}