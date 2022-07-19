namespace AStar;

public abstract class State<TDerived> : IComparable<TDerived>, IEquatable<TDerived>
    where TDerived : State<TDerived> {
    protected int _cost;
    // TODO Set Private
    public int _heuristicCost;

    // TODO Set Private
    public int ExpectedCost => _cost + _heuristicCost;

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