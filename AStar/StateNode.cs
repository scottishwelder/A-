using System.Collections;

namespace AStar;

public class StateNode<T> : IComparable<StateNode<T>>, IEquatable<StateNode<T>>, IEnumerable<T> where T : State<T> {
    private readonly List<StateNode<T>> _children = new();
    private readonly StateNode<T>? _parent;
    public readonly T Data;

    public StateNode(T data, StateNode<T>? parent) {
        Data = data;
        _parent = parent;
    }

    public int CompareTo(StateNode<T>? other) {
        return Data.CompareTo(other?.Data);
    }

    public IEnumerator<T> GetEnumerator() {
        if (_parent is null)
            yield return Data;
        else
            foreach (var element in _parent)
                yield return element;
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public bool Equals(StateNode<T>? other) {
        return Data.Equals(other?.Data);
    }

    public override bool Equals(object? obj) {
        return Equals(obj as StateNode<T>);
    }

    public override int GetHashCode() {
        return Data.GetHashCode();
    }

    public IEnumerable<StateNode<T>> Expand() {
        if (_children.Count != 0) return _children;

        var dataChildren = Data.Children();
        foreach (var dataChild in dataChildren) {
            _children.Add(new StateNode<T>(dataChild, this));
        }

        return _children;
    }
}