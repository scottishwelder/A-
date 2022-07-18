namespace AStar;

public class StateList<T> where T : class, IComparable<T>, IEquatable<T> {
    private class Node {
        public T Data { get; }
        public Node? NextNode;

        public Node(T data) {
            Data = data;
        }

        public Node(T data, Node nextNode) {
            Data = data;
            NextNode = nextNode;
        }
    }

    private Node? _head;
    public int Length { get; private set; } = 0;

    public void Add(T data) {
        var (contains, oldNode) = Contains(data);
        if (contains) {
            if (data.CompareTo(oldNode.Data) < 0) {
                Remove(oldNode);
                Length--;
            }
            else {
                return;
            }
        }
        if (_head == null) {
            _head = new Node(data);
            Length++;
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

        Length++;
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
}