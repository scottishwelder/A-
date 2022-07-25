using System.Collections;

namespace AStar.AdHocCollections;

public partial class StateTree<T> : IEnumerable<T> where T : IComparable<T>, IEquatable<T> {
    private readonly Dictionary<T, Node> _stateLookup = new();
    private Node? _root;
    public int Height => _root?.Height ?? 0;
    public int Count { get; private set; }

    public IEnumerator<T> GetEnumerator() {
        if (_root is null)
            yield break;
        foreach (var element in _root) yield return element;
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }


    // TODO Remove this
    public Dictionary<T, Node> GetStateLookup() {
        return _stateLookup;
    }

    private bool CheckSorting() {
        using var enumerator = GetEnumerator();
        enumerator.MoveNext();
        var previous = enumerator.Current;
        while (enumerator.MoveNext()) {
            var current = enumerator.Current;
            if (current.CompareTo(previous) < 0)
                return false;
            previous = current;
        }

        return true;
    }

    public void Remove(T data) {
        if (_root is null)
            throw new InvalidOperationException("Removing from an empty tree");
        if (!_stateLookup.ContainsKey(data))
            throw new InvalidOperationException("Removing nonexistent element");
        (_root, _) = RemoveFrom(_root, data);
    }

    private static Node Rebalance(Node node) {
        var bf = GetHeight(node.Left) - GetHeight(node.Right);
        switch (bf) {
            case > 1:
                return GetHeight(node.Left!.Left) > GetHeight(node.Left.Right)
                    ? RotateRight(node)
                    : RotateLeftRight(node);
            case < -1:
                return GetHeight(node.Right!.Right) > GetHeight(node.Right.Left)
                    ? RotateLeft(node)
                    : RotateRightLeft(node);
            default:
                node.UpdateHeight();
                return node;
        }
    }

    public override string ToString() {
        return _root is null ? $"StateTree<{typeof(T)}>[]" : $"StateTree<{typeof(T)}>[{_root.ToString()}]";
    }

    private static Node RotateRight(Node node) {
        var l = node.Left!;
        var lr = l.Right;
        l.Right = node;
        node.Left = lr;
        node.UpdateHeight();
        l.UpdateHeight();
        return l;
    }

    private static Node RotateLeft(Node node) {
        var r = node.Right!;
        var rl = r.Left;
        r.Left = node;
        node.Right = rl;
        node.UpdateHeight();
        r.UpdateHeight();
        return r;
    }

    private static Node RotateLeftRight(Node node) {
        node.Left = RotateLeft(node.Left!);
        return RotateRight(node);
    }

    private static Node RotateRightLeft(Node node) {
        node.Right = RotateRight(node.Right!);
        return RotateLeft(node);
    }

    private static int GetHeight(Node? node) {
        return node?.Height ?? 0;
    }
}