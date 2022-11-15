using AStar.AdHocCollections;
using AStar.BaseTypes;

namespace AStar;

public class PuzzleSolver<TState> where TState : State<TState> {
    private readonly HashSet<TState> _exploredStates = new();
    private readonly StateTree<SearchNode<TState>> _frontier = new();
    private int _steps;

    public PuzzleSolver(TState initialState, World<TState> world) {
        _frontier.Add(new SearchNode<TState>(initialState, null, 0, world));
    }

    private SearchNode<TState>? Step() {
        if (_frontier.Count == 0) throw new InvalidOperationException("No Possible solution");

        Log();

        var currentNode = _frontier.Pop();
        if (currentNode.IsObjective) return currentNode;

        _exploredStates.Add(currentNode.State);
        var expansion = currentNode.Expand();
        foreach (var searchNode in expansion)
            if (!_exploredStates.Contains(searchNode.State))
                _frontier.Add(searchNode);

        _steps++;
        return null;
    }

    public SearchNode<TState>? Solve() {
        SearchNode<TState>? solution;
        do
            try {
                solution = Step();
            }
            catch (InvalidOperationException) {
                return null;
            }
        while (solution is null);

        return solution;
    }

    private void Log() {
        Console.Write($"\r{_steps} {_frontier.Count}");
    }
}