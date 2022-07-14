namespace AStar;

public class PuzzleSolver {
    private Tree<PuzzleState> _tree;
    private readonly StateList<PuzzleState> _frontier = new StateList<PuzzleState>();
    private HashSet<PuzzleState> _exploredStates = new HashSet<PuzzleState>();

    public PuzzleSolver(PuzzleState initialState) {
        _tree = new Tree<PuzzleState>(initialState, 4);
    }

    private void Step() {
        var current = _frontier.Pop();
        if (current == null) return;
        var expansion = current.Children();
        foreach (var state in expansion) { }
    }

    //public PuzzleState Solve() {}
}