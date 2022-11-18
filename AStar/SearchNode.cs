using System.Collections;
using AStar.BaseTypes;

namespace AStar;

public class SearchNode<TState> :
    IComparable<SearchNode<TState>>,
    IEquatable<SearchNode<TState>>,
    IEnumerable<TState>
    where TState : State<TState>
{
    private readonly int _cost;
    private readonly int _heuristicCost;
    private readonly SearchNode<TState>? _parent;
    private readonly World<TState> _world;
    public readonly TState State;

    public SearchNode(TState state, SearchNode<TState>? parent, int cost, World<TState> world)
    {
        State = state;
        _parent = parent;
        _cost = cost;
        _world = world;
        _heuristicCost = _world.Heuristics(state);
    }

    private int ExpectedCost => _cost + _heuristicCost;
    public bool IsObjective => State.Equals(_world.Objective);

    public IEnumerable<SearchNode<TState>> Expand() =>
        from child in State.Children()
        select new SearchNode<TState>(child, this, _cost + _world.Cost(child), _world);

    public int CompareTo(SearchNode<TState>? other)
    {
        if (other is null) return 1;
        int compareExpected = ExpectedCost.CompareTo(other.ExpectedCost);
        return compareExpected != 0 ? compareExpected : _heuristicCost.CompareTo(other._heuristicCost);
    }

    public IEnumerator<TState> GetEnumerator()
    {
        yield return State;
        if (_parent is null) yield break;
        foreach (var element in _parent)
            yield return element;
    }

    public bool Equals(SearchNode<TState>? other) => State.Equals(other?.State);
    public override string ToString() => $"SearchNode<{typeof(TState)}>{{{State.ToString()}}}";

    public override int GetHashCode() => State.GetHashCode();
    public override bool Equals(object? obj) => Equals(obj as SearchNode<TState>);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}