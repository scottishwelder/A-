namespace AStar;

public class PuzzleState: IComparable<PuzzleState>, IEquatable<PuzzleState> {
    private readonly int[] _pieces;
    private int cost;

    public PuzzleState() {
        var generator = new Random();
        _pieces = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        _pieces = _pieces.OrderBy(x => x).ToArray();
    }

    public PuzzleState(int[] initialSorting) {
        _pieces = initialSorting;
    }

    public PuzzleState[] Children() {
        return new PuzzleState[4];
    }

    public int CompareTo(PuzzleState? other) {
        return other == null ? 1 : cost.CompareTo(other.cost);
    }

    public bool Equals(PuzzleState? other) {
        return _pieces == other?._pieces;
    }
}