namespace AStar.BaseTypes;

public abstract class State<TDerived> : IEquatable<TDerived> where TDerived : State<TDerived>
{
    public abstract bool Equals(TDerived? other);
    public abstract IEnumerable<TDerived> Children();
    public abstract override int GetHashCode();
    public override bool Equals(object? other) => Equals(other as TDerived);
}