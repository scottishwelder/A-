using AStar.AdHocCollections;

namespace AStar;

public class PuzzleSolver {
    private readonly HashSet<SlidingPuzzleState> _exploredStates = new();
    private readonly StateList<StateNode<SlidingPuzzleState>> _frontier = new();
    private readonly SlidingPuzzleState _objective;

    private int _steps = 0;

    public PuzzleSolver(SlidingPuzzleState initialState, SlidingPuzzleState objective) {
        _objective = objective;
        var root = new StateNode<SlidingPuzzleState>(initialState, null);
        _frontier.Add(root);
    }

    private StateNode<SlidingPuzzleState>? Step() {
        /*for (var node = _frontier._head; node != null; node = node.NextNode) {
            Console.WriteLine(string.Join(',', node.Data.Data._pieces) + " " + node.Data.Data.ExpectedCost + " " + node.Data.Data._heuristicCost);
        }
        Console.WriteLine("");*/

        _steps++;

        //Console.WriteLine(steps + " " + _frontier.Length + " " + _frontier._head?.Data.Data.ExpectedCost);

        var current = _frontier.Pop();
        if (current == null) throw new Exception("No Possible solution");

        Console.WriteLine(_steps + " " + _frontier.Length);

        if (current.Data.Equals(_objective)) return current;
        _exploredStates.Add(current.Data);
        var expansion = current.Expand();
        foreach (var stateNode in expansion) {
            if (!_exploredStates.Contains(stateNode.Data))
                _frontier.Add(stateNode);
        }

        return null;
    }

    public StateNode<SlidingPuzzleState> Solve() {
        var solution = Step();
        while (solution == null) {
            solution = Step();
        }

        return solution;
    }
}