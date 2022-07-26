using System.Collections;

namespace AStar;

public class SearchNode<TState> : IComparable<SearchNode<TState>>,
    IEquatable<SearchNode<TState>>,
    IEnumerable<TState> where TState : State<TState> {
    private readonly int _cost;
    private readonly int _heuristicCost;
    private readonly SearchNode<TState>? _parent;
    private readonly World<TState> _world;
    public readonly TState State;

    private int ExpectedCost => _cost + _heuristicCost;
    public bool IsObjective => State.Equals(_world.Objective);

    public SearchNode(TState state, SearchNode<TState>? parent, int cost, World<TState> world) {
        State = state;
        _parent = parent;
        _cost = cost;
        _heuristicCost = world.Heuristics(state);
        _world = world;
    }

    public int CompareTo(SearchNode<TState>? other) {
        if (other is null) return 1;
        var compareExpected = ExpectedCost.CompareTo(other.ExpectedCost);
        return compareExpected != 0 ? compareExpected : _heuristicCost.CompareTo(other._heuristicCost);
    }

    public IEnumerator<TState> GetEnumerator() {
        if (_parent is null) {
            yield return State;
        }
        else {
            yield return State;
            foreach (var element in _parent)
                yield return element;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public bool Equals(SearchNode<TState>? other) {
        return State.Equals(other?.State);
    }

    public override bool Equals(object? obj) {
        return Equals(obj as SearchNode<TState>);
    }

    public override int GetHashCode() {
        return State.GetHashCode();
    }

    public override string ToString() {
        return $"SearchNode<{typeof(TState)}>{{{State.ToString()}}}";
    }

    public IEnumerable<SearchNode<TState>> Expand() {
        return State.Children().Select(child =>
            new SearchNode<TState>(child, this, _cost + 1, _world));
    }
}