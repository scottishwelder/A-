namespace AStar;

public class StateList<T> {
    private class Node {
        public T Data { get; }
        public Node? _nextNode;

        public Node(T data) {
            Data = data;
        }
    }

    private Node _head = null;

    public Nullable<T> Pop() {
        var fistNode = _head;
        if (fistNode != null) return fistNode.Data;
        else return null;
    }
}