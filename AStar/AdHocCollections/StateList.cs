namespace AStar.AdHocCollections;

public class StateList<T> where T : class, IComparable<T>, IEquatable<T> {
    private Node? _head;
    public int Length { get; private set; }

    public void Add(T data) {
        Length++;
        var (contains, oldNode) = Contains(data);
        if (contains) {
            if (data.CompareTo(oldNode.Data) < 0) {
                Remove(oldNode);
            }
            else {
                Length--;
                return;
            }
        }

        if (_head == null) {
            _head = new Node(data);
            return;
        }

        if (data.CompareTo(_head.Data) < 0) {
            _head = new Node(data, _head);
            return;
        }

        Node node;
        for (node = _head; node.NextNode != null; node = node.NextNode) {
            if (data.CompareTo(node.NextNode.Data) > 0) continue;
            var newNode = new Node(data, node.NextNode);
            node.NextNode = newNode;
            break;
        }

        if (node.NextNode == null) {
            var newNode = new Node(data);
            node.NextNode = newNode;
        }
    }

    public T? Pop() {
        if (_head == null)
            return null;
        Length--;
        var firstNode = _head;
        _head = _head.NextNode;
        return firstNode.Data;
    }

    private void Remove(Node node) {
        if (_head == null) return;
        Length--;
        for (var currentNode = _head; currentNode.NextNode != null; currentNode = currentNode.NextNode) {
            if (!node.Data.Equals(currentNode.NextNode.Data)) continue;
            currentNode.NextNode = currentNode.NextNode.NextNode;
            return;
        }
    }

    private (bool, Node?) Contains(T data) {
        for (var node = _head; node != null; node = node.NextNode)
            if (data.Equals(node.Data))
                return (true, node);

        return (false, null);
    }

    private class Node {
        public Node? NextNode;

        public Node(T data) {
            Data = data;
        }

        public Node(T data, Node nextNode) {
            Data = data;
            NextNode = nextNode;
        }

        public T Data { get; }
    }
}