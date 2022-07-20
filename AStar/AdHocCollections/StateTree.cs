using System.Collections;
using System.Text;

namespace AStar.AdHocCollections;

public class StateTree<T> : IEnumerable<T> where T : IComparable<T> {
    private Node? _root;
    public int Height => _root?.Height ?? 0;

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

    public void Add(T data) {
        _root = AddTo(_root, data);
    }

    public void Add(IEnumerable<T> data) {
        foreach (var element in data) {
            Add(element);
        }
    }

    public override string ToString() {
        return _root is null ? $"StateTree<{nameof(T)}>[]" : $"StateTree<{nameof(T)}>[{_root.ToString()}]";
    }

    private static Node AddTo(Node? node, T data) {
        if (node == null) {
            return new Node(data, null, null);
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

    private class Node {
        public readonly T Data;
        public int Height = 1;
        public Node? Left;
        public Node? Right;

        public Node(T data, Node? left, Node? right) {
            Data = data;
            Left = left;
            Right = right;
        }

        public void UpdateHeight() {
            Height = Math.Max(Left?.Height ?? 0, Right?.Height ?? 0) + 1;
        }

        public override string ToString() {
            var builder = new StringBuilder();
            var left = Left?.ToString();
            var right = Right?.ToString();
            if (left is not null)
                builder.Append(left + ", ");
            builder.Append(Data);
            if (right is not null)
                builder.Append(", " + right);
            return builder.ToString();
        }

        public IEnumerator<T> GetEnumerator() {
            if (Left is not null) {
                foreach (var element in Left) {
                    yield return element;
                }
            }

            yield return Data;

            if (Right is not null) {
                foreach (var element in Right) {
                    yield return element;
                }
            }
        }
    }
}