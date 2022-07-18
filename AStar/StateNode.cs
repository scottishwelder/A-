namespace AStar;

public class StateNode<T> : IComparable<StateNode<T>>, IEquatable<StateNode<T>> where T : State<T>, IComparable<T>, IEquatable<T> {
    public readonly T Data;
    private readonly List<StateNode<T>> _children = new();
    private readonly StateNode<T>? _parent;

    public StateNode(T data, StateNode<T>? parent) {
        Data = data;
        _parent = parent;
    }

    public int CompareTo(StateNode<T>? other) {
        return Data.CompareTo(other?.Data);
    }

    public bool Equals(StateNode<T>? other) {
        return Data.Equals(other?.Data);
    }

    public IEnumerable<StateNode<T>> Expand() {
        var dataChildren = Data.Children();
        foreach (var dataChild in dataChildren) {
            _children.Add(new StateNode<T>(dataChild, this));
        }

        return _children;
    }
}