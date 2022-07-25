using System.Diagnostics.CodeAnalysis;

namespace AStar.AdHocCollections;

public partial class StateTree<T> {
    // ReSharper disable once MemberCanBePrivate.Global
    public void Add(T data) {
        var existingNode = Contains(data);
        if (existingNode is not null) {
            if (data.CompareTo(existingNode.Data) < 0)
                Remove(existingNode.Data);
            else
                return;
        }

        Count++;
        AddTo(ref _root, data);
    }

    public void Add(IEnumerable<T> data) {
        foreach (var element in data) Add(element);
    }

    private void AddTo(ref Node? node, T data) {
        if (node is null) {
            node = new Node(data, null, null);
            _stateLookup.Add(data, node);
            return;
        }

        if (data.CompareTo(node.Data) > 0)
            AddTo(ref node.Right, data);
        else
            AddTo(ref node.Left, data);

        Rebalance(ref node);
    }

    private Node? Contains(T data) {
        _stateLookup.TryGetValue(data, out var response);
        return response;
    }

    private bool RemoveFrom([DisallowNull] ref Node? node, T data) {
        if (data.Equals(node.Data)) {
            Count--;
#if DEBUG
            if (!_stateLookup.Remove(data))
                throw new Exception($"Key \"{data}\" was not found in the state lookup");
#else
            _stateLookup.Remove(data);
#endif
            if (node.Left is null) {
                node = node.Right;
                return true;
            }

            if (node.Right is null) {
                node = node.Left;
                return true;
            }

            var element = PopFrom(ref node.Right);
            node = new Node(element, node.Left, node.Right);
            Rebalance(ref node);
            return true;
        }

        var found = false;
        switch (data.CompareTo(node.Data)) {
            case > 0:
                if (node.Right is not null)
                    found = RemoveFrom(ref node.Right, data);
                break;
            case < 0:
                if (node.Left is not null)
                    found = RemoveFrom(ref node.Left, data);
                break;
            case 0:
                var foundLeft = false;
                var foundRight = false;
                if (node.Left is not null)
                    foundLeft = RemoveFrom(ref node.Left, data);

                if (!foundLeft && node.Right is not null)
                    foundRight = RemoveFrom(ref node.Right, data);

                found = foundLeft || foundRight;
                break;
        }

        Rebalance(ref node);
        return found;
    }
}