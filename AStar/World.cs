namespace AStar;

public abstract class World<TState> {
    protected readonly TState Objective;

    protected World(TState objective) {
        Objective = objective;
    }

    protected abstract int Heuristics(TState state);
    public abstract IEnumerable<TState> Children(TState state);
}