namespace AStar.AdHocCollections;

public partial class StateTree<T> {
    // ReSharper disable once MemberCanBePrivate.Global
    public void Add(T data) {
        if (!CheckSorting())
            throw new Exception("Not sorted before");
        var existingNode = Contains(data);
        if (existingNode is not null) {
            if (data.CompareTo(existingNode.Data) < 0) {
                var previousCount = Count;
                (_root, var found) = RemoveFrom(_root!, existingNode.Data);
                if (!found)
                    if (((IEnumerable<T>)this).Contains(data)) {
                        Console.WriteLine(this);
                        Console.WriteLine();
                        Console.WriteLine(data);
                        (_root, _) = RemoveFrom(_root!, existingNode.Data);
                        throw new Exception("This should be here");
                    }
                    else
                        throw new Exception("This really is not here");
                if (previousCount - 1 != Count)
                    throw new Exception($"p: {previousCount}, c: {Count}");
            }
            else {
                return;
            }
        }

        Count++;
        _root = AddTo(_root, data);
        
        if (!CheckSorting())
            throw new Exception("Not sorted after");
    }

    public void Add(IEnumerable<T> data) {
        foreach (var element in data) Add(element);
    }

    private Node AddTo(Node? node, T data) {
        if (node == null) {
            var newNode = new Node(data, null, null);
            _stateLookup.Add(data, newNode);
            return newNode;
        }

        if (data.CompareTo(node.Data) > 0)
            node.Right = AddTo(node.Right, data);
        else
            node.Left = AddTo(node.Left, data);

        return Rebalance(node);
    }

    private Node? Contains(T data) {
        _stateLookup.TryGetValue(data, out var response);
        return response;
    }

    private (Node?, bool) RemoveFrom(Node node, T data) {
        if (data.Equals(node.Data)) {
            Count--;
#if DEBUG
            if (!_stateLookup.Remove(data))
                throw new Exception($"Key \"{data}\" was not found in the state lookup");
#else
            _stateLookup.Remove(data);
#endif
            if (node.Left is null)
                return (node.Right, true);
            if (node.Right is null)
                return (node.Left, true);

            (var element, node.Right) = PopFrom(node.Right);
            var successor = new Node(element, node.Left, node.Right);
            return (Rebalance(successor), true);
        }

        bool found;
        switch (data.CompareTo(node.Data)) {
            case > 0:
                (node.Right, found) = RemoveFrom(node.Right!, data);
                break;
            case < 0:
                (node.Left, found) = RemoveFrom(node.Left!, data);
                break;
            case 0:
                var foundRight = false;
                var foundLeft = false;
                if (node.Left is not null && data.CompareTo(node.Left.Data) == 0)
                    (node.Left, foundLeft) = RemoveFrom(node.Left, data);

                if (!foundLeft && node.Right is not null && data.CompareTo(node.Right.Data) == 0)
                    (node.Right, foundRight) = RemoveFrom(node.Right, data);

                found = foundLeft || foundRight;
                break;
        }

        return (Rebalance(node), found);
    }
}