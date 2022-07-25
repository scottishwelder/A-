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
        /*for (var node = _frontier._head; node != null; node = node.NextNode) {
            Console.WriteLine(string.Join(',', node.Data.Data._pieces) + " " + node.Data.Data.ExpectedCost + " " + node.Data.Data._heuristicCost);
        }
        Console.WriteLine("");*/

        _steps++;

        //Console.WriteLine(steps + " " + _frontier.Length + " " + _frontier._head?.Data.Data.ExpectedCost);

        var current = _frontier.Pop();
        if (current == null) throw new Exception("No Possible solution");

        Console.Write("\r" + _steps + " " + _frontier.Count);

        if (current.Data.Equals(_objective)) return current;
        _exploredStates.Add(current.Data);
        var expansion = current.Expand();
        foreach (var stateNode in expansion)
            if (!_exploredStates.Contains(stateNode.Data))
                _frontier.Add(stateNode);

        return null;
    }

    public SearchNode<TState> Solve() {
        var solution = Step();
        while (solution == null) solution = Step();
        Console.WriteLine("");

        return solution;
    }
}