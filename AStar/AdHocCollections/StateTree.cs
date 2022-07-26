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
        if (!_stateLookup.ContainsKey(data))
            throw new InvalidOperationException("Removing nonexistent element");
        if (_root is not null)
            RemoveFrom(ref _root, data);
        else
            throw new InvalidOperationException("Removing from an empty tree");
    }

    private static void Rebalance(ref Node node) {
        var bf = GetHeight(node.Left) - GetHeight(node.Right);
        switch (bf) {
            case > 1:
                if (GetHeight(node.Left!.Left) > GetHeight(node.Left.Right))
                    RotateRight(ref node);
                else
                    RotateLeftRight(ref node);
                break;
            case < -1:
                if (GetHeight(node.Right!.Right) > GetHeight(node.Right.Left))
                    RotateLeft(ref node);
                else
                    RotateRightLeft(ref node);
                break;
            default:
                node.UpdateHeight();
                break;
        }
    }

    public override string ToString() {
        return _root is null ? $"StateTree<{typeof(T)}>[]" : $"StateTree<{typeof(T)}>[{_root.ToString()}]";
    }

    private static void RotateRight(ref Node node) {
        var l = node.Left!;
        var lr = l.Right;
        l.Right = node;
        node.Left = lr;
        node.UpdateHeight();
        l.UpdateHeight();
        node = l;
    }

    private static void RotateLeft(ref Node node) {
        var r = node.Right!;
        var rl = r.Left;
        r.Left = node;
        node.Right = rl;
        node.UpdateHeight();
        r.UpdateHeight();
        node = r;
    }

    private static void RotateLeftRight(ref Node node) {
        RotateLeft(ref node.Left!);
        RotateRight(ref node);
    }

    private static void RotateRightLeft(ref Node node) {
        RotateRight(ref node.Right!);
        RotateLeft(ref node);
    }

    private static int GetHeight(Node? node) {
        return node?.Height ?? 0;
    }
}