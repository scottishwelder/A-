namespace AStar;

public class Tree<T> {
    private class RootNode {
        private readonly T _data;
        private readonly Node?[] _children;

        public RootNode(T data, int numberOfChildren) {
            _data = data;
            _children = new Node[numberOfChildren];
        }
    }

    private class Node : RootNode {
        private readonly Node _parent;
        public Node(T data, Node parent, int numberOfChildren) : base(data, numberOfChildren) {
            _parent = parent;
        }
    }

    private RootNode _root;

    public Tree(T rootData, int numberOfChildren) {
        _root = new RootNode(rootData, numberOfChildren);
    }
}