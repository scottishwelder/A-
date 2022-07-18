namespace AStar;

public class SlidingPuzzleState : State<SlidingPuzzleState> {
    private enum Direction {
        North,
        West,
        South,
        East
    }

    public readonly int[] _pieces;

    public SlidingPuzzleState(int cost) : base(cost) {
        var generator = new Random();
        _pieces = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        _pieces = _pieces.OrderBy(x => generator.Next()).ToArray();
        PostConstruction();
    }

    public SlidingPuzzleState(int cost, int[] pieces) : base(cost) {
        _pieces = pieces;
        PostConstruction();
    }

    protected override int HeuristicCost() {
        var sum = 0;
        for (var i = 0; i < _pieces.Length; i++) {
            if (_pieces[i] == 0) continue;
            var currentPosition = (i % 3, i / 3);
            var finalPosition = (_pieces[i] % 3, _pieces[i] / 3);
            sum += Math.Abs(currentPosition.Item1 - finalPosition.Item1) +
                   Math.Abs(currentPosition.Item2 - finalPosition.Item2);
        }

        return sum;
    }

    public override IEnumerable<SlidingPuzzleState> Children() {
        var zeroPosition = Array.IndexOf(_pieces, 0);
        var zeroCoordinate = (zeroPosition % 3, zeroPosition / 3);
        var childrenDirections = new List<Direction>();

        if (zeroCoordinate.Item2 > 0)
            childrenDirections.Add(Direction.North);
        if (zeroCoordinate.Item1 > 0)
            childrenDirections.Add(Direction.West);
        if (zeroCoordinate.Item1 < 2)
            childrenDirections.Add(Direction.South);
        if (zeroCoordinate.Item1 < 2)
            childrenDirections.Add(Direction.East);

        return childrenDirections.Select(ChildrenFromDirection);
    }

    private SlidingPuzzleState ChildrenFromDirection(Direction direction) {
        var newPieces = (int[])_pieces.Clone();
        var zeroPosition = Array.IndexOf(_pieces, 0);
        var zeroCoordinate = (zeroPosition % 3, zeroPosition / 3);
        var movingPieceCoordinate = direction switch {
            Direction.North => (zeroCoordinate.Item1, zeroCoordinate.Item1 - 1),
            Direction.West => (zeroCoordinate.Item1 - 1, zeroCoordinate.Item1),
            Direction.South => (zeroCoordinate.Item1, zeroCoordinate.Item1 + 1),
            Direction.East => (zeroCoordinate.Item1 + 1, zeroCoordinate.Item1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

        var movingPiecePosition = movingPieceCoordinate.Item2 * 3 + movingPieceCoordinate.Item1;

        newPieces[zeroPosition] = newPieces[movingPiecePosition];
        newPieces[movingPiecePosition] = 0;

        return new SlidingPuzzleState(_cost + 1, newPieces);
    }

    public override int CompareTo(SlidingPuzzleState? other) {
        return other == null ? 1 : _cost.CompareTo(other._cost);
    }

    public override bool Equals(SlidingPuzzleState? other) {
        return other != null && _pieces.SequenceEqual(other._pieces);
    }

    public override int GetHashCode() {
        return _pieces.GetHashCode();
    }
}