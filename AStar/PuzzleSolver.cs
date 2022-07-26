using AStar.AdHocCollections;

namespace AStar;

public class PuzzleSolver<TState> where TState : State<TState> {
    private readonly HashSet<TState> _exploredStates = new();
    private readonly StateTree<SearchNode<TState>> _frontier = new();
    
    private int _steps;

    public PuzzleSolver(TState initialState, World<TState> world) {
        var root = new SearchNode<TState>(initialState, null, 0, world);
        _frontier.Add(root);
    }

    private SearchNode<TState>? Step() {
        if (_frontier.Count == 0) throw new InvalidOperationException("No Possible solution");

        Console.Write("\r" + _steps + " " + _frontier.Count);

        var current = _frontier.Pop();
        if (current.IsObjective) return current;

        _exploredStates.Add(current.State);
        var expansion = current.Expand();
        foreach (var stateNode in expansion)
            if (!_exploredStates.Contains(stateNode.State))
                _frontier.Add(stateNode);

        _steps++;
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