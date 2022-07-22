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
        foreach (var element in _root) {
            yield return element;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public override string ToString() {
        return _root is null ? $"StateTree<{nameof(T)}>[]" : $"StateTree<{nameof(T)}>[{_root.ToString()}]";
    }

    private static Node RotateRight(Node node) {
        Console.WriteLine("Rotating Right node " + node.Data);
        var l = node.Left;
        if (l == null) throw new ArgumentException("Rotating right a node with no left child.");
        var lr = l.Right;
        l.Right = node;
        node.Left = lr;
        node.UpdateHeight();
        l.UpdateHeight();
        return l;
    }

    private static Node RotateLeft(Node node) {
        Console.WriteLine("Rotating Left node " + node.Data);
        var r = node.Right;
        if (r == null) throw new ArgumentException("Rotating left a node with no right child.");
        var rl = r.Left;
        r.Left = node;
        node.Right = rl;
        node.UpdateHeight();
        r.UpdateHeight();
        return r;
    }

    private static Node RotateLeftRight(Node node) {
        if (node.Left == null) throw new ArgumentException("Rotating right a node with no left child");
        node.Left = RotateLeft(node.Left);
        return RotateRight(node);
    }

    private static Node RotateRightLeft(Node node) {
        if (node.Right == null) throw new ArgumentException("Rotating left a node with no right child");
        node.Right = RotateRight(node.Right);
        return RotateLeft(node);
    }

    private static int GetHeight(Node? node) {
        return node?.Height ?? 0;
    }
}