namespace AStar;

public abstract class State<TDerived> : IComparable<TDerived>, IEquatable<TDerived>
    where TDerived : State<TDerived> {
    protected readonly int Cost;
    protected int HeuristicCost;

    protected State(int cost) {
        Cost = cost;
    }

    protected int ExpectedCost => Cost + HeuristicCost;
    public abstract int CompareTo(TDerived? other);
    public abstract bool Equals(TDerived? other);
    public abstract override bool Equals(object? other);
    public abstract override int GetHashCode();

    protected void PostConstruction() {
        HeuristicCost = Heuristics();
    }

    protected abstract int Heuristics();
    public abstract IEnumerable<TDerived> Children();
}