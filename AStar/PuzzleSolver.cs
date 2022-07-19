namespace AStar;

public class PuzzleSolver {
    private StateNode<SlidingPuzzleState> _root;
    private SlidingPuzzleState _objective;
    private readonly StateList<StateNode<SlidingPuzzleState>> _frontier = new();
    private readonly HashSet<SlidingPuzzleState> _exploredStates = new();

    private int steps = 0;

    public PuzzleSolver(SlidingPuzzleState initialState, SlidingPuzzleState objective) {
        _root = new StateNode<SlidingPuzzleState>(initialState, null);
        _objective = objective;
        _frontier.Add(_root);
    }

    private StateNode<SlidingPuzzleState>? Step() {
        /*for (var node = _frontier._head; node != null; node = node.NextNode) {
            Console.WriteLine(string.Join(',', node.Data.Data._pieces) + " " + node.Data.Data.ExpectedCost + " " + node.Data.Data._heuristicCost);
        }
        Console.WriteLine("");*/

        steps++;

        //Console.WriteLine(steps + " " + _frontier.Length + " " + _frontier._head?.Data.Data.ExpectedCost);

        var current = _frontier.Pop();
        if (current == null) throw new Exception("No Possible solution");

        Console.WriteLine(steps + " " + _frontier.Length + " " + current.Data.ExpectedCost + " " +
                          current.Data._heuristicCost);

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