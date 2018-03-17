using System.Text;

namespace phirSOFT.TopologicalComparison
{
    public class DirectedGraph<T> : IDirectedGraph<T>
    {
        private readonly DirectedGraphNode<T> _root;

        public DirectedGraph(T rootValue)
        {
            _root = new DirectedGraphNode<T>(rootValue);
        }

        ITreeNode<T> ITree<T>.Root => _root;

        public IDirectedGraphNode<T> Root => _root;

    }
}
