namespace AStar.AdHocCollections;

public partial class StateTree<T> {
    public void Add(T data) {
        var node = Contains(data);
        if (node is not null) {
            if (data.CompareTo(node.Data) < 0) {
                Remove(node.Data);
            }
            else {
                return;
            }
        }

        Count++;

        _root = AddTo(_root, data);
    }

    public void Add(IEnumerable<T> data) {
        foreach (var element in data) {
            Add(element);
        }
    }

    private Node AddTo(Node? node, T data) {
        if (node == null) {
            var newNode = new Node(data, null, null);
            _stateLookup.Add(data, newNode);
            return newNode;
        }

        if (data.CompareTo(node.Data) > 0) {
            node.Right = AddTo(node.Right, data);
            var bf = GetHeight(node.Left) - GetHeight(node.Right);
            switch (bf) {
                case < -1:
                    return GetHeight(node.Right.Right) > GetHeight(node.Right.Left)
                        ? RotateLeft(node)
                        : RotateRightLeft(node);
                case > 1:
                    throw new Exception(
                        "An insertion on the right required a rotation from the left. Something is wrong.");
                default:
                    node.UpdateHeight();
                    return node;
            }
        }
        else {
            node.Left = AddTo(node.Left, data);
            var bf = GetHeight(node.Left) - GetHeight(node.Right);
            switch (bf) {
                case > 1:
                    return GetHeight(node.Left.Left) > GetHeight(node.Left.Right)
                        ? RotateRight(node)
                        : RotateLeftRight(node);
                case < -1:
                    throw new Exception(
                        "An insertion on the left required a rotation from the right. Something is wrong.");
                default:
                    node.UpdateHeight();
                    return node;
            }
        }
    }

    private Node? Contains(T data) {
        //throw new NotImplementedException();
        _stateLookup.TryGetValue(data, out var response);
        return response;
    }

    private void Remove(T data) {
        throw new NotImplementedException();
    }
}