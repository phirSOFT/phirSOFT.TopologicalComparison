using System;
using System.Collections.Generic;
using System.Text;

namespace phirSOFT.TopologicalComparison
{
    public interface IBidirectionalTreeNode<TValue> : ITreeNode<TValue, IBidirectionalTreeNode<TValue>>
    {
        IBidirectionalTreeNode<TValue> Parent {get;} 
    }
}
