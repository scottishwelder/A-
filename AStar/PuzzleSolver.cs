namespace AStar;

public class PuzzleSolver {
    private StateNode<SlidingPuzzleState> _root;
    private readonly StateList<StateNode<SlidingPuzzleState>> _frontier = new();
    private HashSet<SlidingPuzzleState> _exploredStates = new();

    public PuzzleSolver(SlidingPuzzleState initialState) {
        _root = new StateNode<SlidingPuzzleState>(initialState, null);
        _frontier.Add(_root);
    }

    private void Step() {
        var current = _frontier.Pop();
        if (current == null) return;
        _exploredStates.Add(current.Data);
        var expansion = current.Expand();
        foreach (var stateNode in expansion) {
            if (!_exploredStates.Contains(stateNode.Data))
                _frontier.Add(stateNode);
        }
    }

    //public PuzzleState Solve() {}
}