namespace AStar.BaseTypes;

public abstract class World<TState> where TState : State<TState> {
    public readonly TState Objective;

    protected World(TState objective) {
        Objective = objective;
    }

    public abstract int Heuristics(TState state);
}