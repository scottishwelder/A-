using AStar.AdHocCollections;

namespace AStar;

public class PuzzleSolver<TState> where TState : State<TState> {
    private readonly HashSet<TState> _exploredStates = new();

    //private readonly StateList<SearchNode<TState>> _frontier = new();
    private readonly StateTree<SearchNode<TState>> _frontier = new();
    private readonly TState _objective;

    private int _steps;

    public PuzzleSolver(TState initialState, TState objective) {
        _objective = objective;
        var root = new SearchNode<TState>(initialState, null);
        _frontier.Add(root);
    }

    private SearchNode<TState>? Step() {
        _steps++;

        if (_frontier.Count == 0) throw new InvalidOperationException("No Possible solution");
        var current = _frontier.Pop();

        Console.Write("\r" + _steps + " " + _frontier.Count);

        if (current.Data.Equals(_objective)) return current;
        _exploredStates.Add(current.Data);
        var expansion = current.Expand();
        foreach (var stateNode in expansion)
            if (!_exploredStates.Contains(stateNode.Data))
                _frontier.Add(stateNode);

        return null;
    }

    public SearchNode<TState>? Solve() {
        SearchNode<TState>? solution;
        do {
            try {
                solution = Step();
            }
            catch (InvalidOperationException) {
                return null;
            }
        } while (solution is null);

        Console.WriteLine("");

        return solution;
    }
}