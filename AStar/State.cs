namespace AStar;

public abstract class State<TDerived> : IComparable<TDerived>, IEquatable<TDerived>
    where TDerived : State<TDerived> {
    public readonly int Cost;
    protected readonly int HeuristicCost;

    protected State(int cost, int heuristicCost) {
        Cost = cost;
        HeuristicCost = heuristicCost;
    }

    protected int ExpectedCost => Cost + HeuristicCost;
    public abstract int CompareTo(TDerived? other);
    public abstract bool Equals(TDerived? other);
    public abstract override bool Equals(object? other);
    public abstract override int GetHashCode();
}