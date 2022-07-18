namespace AStar;

public abstract class State<TDerived> : IComparable<TDerived>, IEquatable<TDerived>
    where TDerived : State<TDerived> {
    protected int _cost;
    private int _heuristicCost;

    protected int ExpectedCost => _cost + _heuristicCost;

    protected State(int cost) {
        _cost = cost;
    }

    protected void PostConstruction() {
        _heuristicCost = HeuristicCost();
    }

    protected abstract int HeuristicCost();
    public abstract IEnumerable<TDerived> Children();
    public abstract int CompareTo(TDerived? other);
    public abstract bool Equals(TDerived? other);
    public abstract override int GetHashCode();
}