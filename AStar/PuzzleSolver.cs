namespace AStar;

public class PuzzleSolver {
    private Tree<PuzzleState> _tree;
    private SortedSet<PuzzleState> _frontier = new SortedSet<PuzzleState>();
    private HashSet<PuzzleState> _exploredStates = new HashSet<PuzzleState>();

    public PuzzleSolver(PuzzleState initialState) {
        _tree = new Tree<PuzzleState>(initialState, 4);
    }

    private void Step() {
        var current = _frontier.First();
        var expansion = current.Children();
        foreach (var state in expansion) {
            AddToFrontier(state);
        }
    }

    private void AddToFrontier(PuzzleState state) {
        _frontier.Add(state);
    }

    //public PuzzleState Solve() {}
}