using System.Collections;

namespace AStar;

public class SearchNode<T> : IComparable<SearchNode<T>>, IEquatable<SearchNode<T>>, IEnumerable<T> where T : State<T> {
    private readonly SearchNode<T>? _parent;
    public readonly T Data;

    public SearchNode(T data, SearchNode<T>? parent) {
        Data = data;
        _parent = parent;
    }

    public int CompareTo(SearchNode<T>? other) {
        return Data.CompareTo(other?.Data);
    }

    public IEnumerator<T> GetEnumerator() {
        if (_parent is null) {
            yield return Data;
        }
        else {
            yield return Data;
            foreach (var element in _parent)
                yield return element;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public bool Equals(SearchNode<T>? other) {
        return Data.Equals(other?.Data);
    }

    public override bool Equals(object? obj) {
        return Equals(obj as SearchNode<T>);
    }

    public override int GetHashCode() {
        return Data.GetHashCode();
    }

    public override string ToString() {
        return $"SearchNode<{typeof(T)}>{{{Data.ToString()}}}";
    }

    public IEnumerable<SearchNode<T>> Expand() {
        return Data.Children().Select(child => new SearchNode<T>(child, this));
    }
}