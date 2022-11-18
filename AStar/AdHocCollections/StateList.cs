namespace AStar.AdHocCollections;

/// <summary>
/// Sorted linked list with special behaviour to handle the A* search nodes
/// </summary>
/// <typeparam name="T">Type of items to be stored by the collection</typeparam>
[Obsolete("The linear complexity of this linked list is too much for the application. Use StateTree instead")]
public class StateList<T> where T : class, IComparable<T>, IEquatable<T>
{
    private Node? _head;
    public int Length { get; private set; }

    public void Add(T data)
    {
        Length++;
        var oldNode = Contains(data);
        if (oldNode is not null)
        {
            if (data.CompareTo(oldNode.Data) < 0)
            {
                // List already contains the data and it should be replaced.
                Remove(oldNode);
            }
            else
            {
                // List already contains the data and nothing should be done.
                Length--;
                return;
            }
        }

        if (_head is null)
        {
            _head = new Node(data);
            return;
        }

        if (data.CompareTo(_head.Data) < 0)
        {
            _head = new Node(data, _head);
            return;
        }

        Node node;
        for (node = _head; node.NextNode is not null; node = node.NextNode)
        {
            if (data.CompareTo(node.NextNode.Data) > 0) continue;
            var newNode = new Node(data, node.NextNode);
            node.NextNode = newNode;
            break;
        }

        if (node.NextNode is null)
        {
            var newNode = new Node(data);
            node.NextNode = newNode;
        }
    }

    public T? Pop()
    {
        if (_head is null)
            return null;
        Length--;
        var firstNode = _head;
        _head = _head.NextNode;
        return firstNode.Data;
    }

    private void Remove(Node node)
    {
        if (_head is null) return;
        Length--;
        for (var currentNode = _head; currentNode.NextNode is not null; currentNode = currentNode.NextNode)
        {
            if (!node.Data.Equals(currentNode.NextNode.Data)) continue;
            currentNode.NextNode = currentNode.NextNode.NextNode;
            return;
        }
    }

    private Node? Contains(T data)
    {
        for (var node = _head; node is not null; node = node.NextNode)
            if (data.Equals(node.Data))
                return node;

        return null;
    }

    private class Node
    {
        public Node? NextNode;

        public Node(T data)
        {
            Data = data;
        }

        public Node(T data, Node nextNode)
        {
            Data = data;
            NextNode = nextNode;
        }

        public T Data { get; }
    }
}