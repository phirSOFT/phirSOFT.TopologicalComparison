using System;
using System.Collections.Generic;
using System.Linq;

namespace phirSOFT.TopologicalComparison
{
    public class DirectedGraphNode<T> : IDirectedGraphNode<T>
    {
        private readonly HashSet<DirectedGraphNode<T>> _children;
        private readonly HashSet<DirectedGraphNode<T>> _parents;
        private int _distance;

        internal DirectedGraphNode(T node, bool root = false)
        {
            Value = node;
            Distance = root ? 0 : int.MaxValue;
            _children = new HashSet<DirectedGraphNode<T>>();
            _parents = new HashSet<DirectedGraphNode<T>>();
        }

        public int Distance
        {
            get => _distance;
            private set
            {
                if (_distance == value) return;
                var old = _distance;
                _distance = value;

                foreach (var child in _children)
                {
                    child.UpdateDistance(value, old);
                }
            }
        }

        private void AddParent(DirectedGraphNode<T> parent)
        {
            _parents.Add(parent);
            Distance = Math.Min(Distance, parent.Distance + 1);
        }

        private void RemoveParent(DirectedGraphNode<T> parent)
        {
            _parents.Remove(parent);
            if (_parents.Count > 0)
                Distance = _parents.Min(node => node.Distance) + 1;
            else
                Distance = int.MaxValue;
        }

        public T Value { get; set; }
        public IEnumerable<IDirectedGraphNode<T>> Children => _children.AsEnumerable();
        ITreeNode<T> ITreeNode<T>.AddChild(T node)
        {
            return AddChild(node);
        }

        public void Detach(ITreeNode<T> child)
        {
            Detach(child as DirectedGraphNode<T>);
        }

        public void Attach(ITreeNode<T> child)
        {
            Attach(child as DirectedGraphNode<T>);
        }

        IEnumerable<ITreeNode<T>> ITreeNode<T>.Children => Children;

        public IDirectedGraphNode<T> AddChild(T node)
        {
            var newNode = new DirectedGraphNode<T>(node);
            Attach(newNode);
            return newNode;
        }

        public void Detach(IDirectedGraphNode<T> child)
        {
            if (!(child is DirectedGraphNode<T> directedChid))
                throw new InvalidOperationException();

            _children.Remove(directedChid);
            directedChid.RemoveParent(this);
        }

        public void Attach(IDirectedGraphNode<T> child)
        {
            if (!(child is DirectedGraphNode<T> directedChid))
                throw new InvalidOperationException();

            _children.Add(directedChid);
            directedChid.AddParent(this);
        }

        private void UpdateDistance(int distance, int oldDistance)
        {
            if (oldDistance + 1 == distance && distance > oldDistance)
                Distance = _parents.Min(node => node.Distance) + 1;
            else
                Distance = Math.Min(Distance, distance + 1);
        }

        public IEnumerable<IDirectedGraphNode<T>> Parents => _parents.AsEnumerable();
    }
}