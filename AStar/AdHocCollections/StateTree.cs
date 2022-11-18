using System.Collections;

namespace AStar.AdHocCollections;

/// <summary>
/// AVL Tree with special behaviour to handle the A* search nodes
/// Partial definition 1 of 5
/// </summary>
/// <typeparam name="T">The type of items to store in this collection</typeparam>
public partial class StateTree<T> : IEnumerable<T> where T : IComparable<T>, IEquatable<T>
{
    private readonly Dictionary<T, Node> _stateLookup = new();
    private Node? _root;

    public int Height => GetHeight(_root);
    public int Count { get; private set; }

    // Used for testing
    public IEnumerator<T> GetEnumerator()
    {
        if (_root is null)
            yield break;
        foreach (var element in _root) yield return element;
    }

    public void Remove(T data)
    {
        if (!_stateLookup.ContainsKey(data))
            throw new InvalidOperationException("Removing nonexistent element");
        if (_root is null)
            throw new InvalidOperationException("Removing from an empty tree");
        RemoveFrom(ref _root, data);
    }

    public override string ToString() =>
        _root is null ? $"StateTree<{typeof(T)}>[]" : $"StateTree<{typeof(T)}>[{_root.ToString()}]";

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    private static int GetHeight(Node? node) => node?.Height ?? 0;
}