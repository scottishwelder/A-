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

    //TODO Lower Redundancy
    public override IEnumerable<SlidingPuzzleState> Children() {
        var zeroPosition = Array.IndexOf(_pieces, 0);
        var zeroCoordinate = (zeroPosition % 3, zeroPosition / 3);

        var children = new List<SlidingPuzzleState>();

        if (zeroCoordinate.Item2 > 0) {
            var newPieces = (int[])_pieces.Clone();
            var movingPieceCoordinate = (zeroCoordinate.Item1, zeroCoordinate.Item2 - 1);
            var movingPiecePosition = movingPieceCoordinate.Item2 * 3 + movingPieceCoordinate.Item1;
            newPieces[zeroPosition] = newPieces[movingPiecePosition];
            newPieces[movingPiecePosition] = 0;
            children.Add(new SlidingPuzzleState(_cost + 1, newPieces));
        }

        if (zeroCoordinate.Item1 > 0) {
            var newPieces = (int[])_pieces.Clone();
            var movingPieceCoordinate = (zeroCoordinate.Item1 - 1, zeroCoordinate.Item2);
            var movingPiecePosition = movingPieceCoordinate.Item2 * 3 + movingPieceCoordinate.Item1;
            newPieces[zeroPosition] = newPieces[movingPiecePosition];
            newPieces[movingPiecePosition] = 0;
            children.Add(new SlidingPuzzleState(_cost + 1, newPieces));
        }

        if (zeroCoordinate.Item2 < 2) {
            var newPieces = (int[])_pieces.Clone();
            var movingPieceCoordinate = (zeroCoordinate.Item1, zeroCoordinate.Item2 + 1);
            var movingPiecePosition = movingPieceCoordinate.Item2 * 3 + movingPieceCoordinate.Item1;
            newPieces[zeroPosition] = newPieces[movingPiecePosition];
            newPieces[movingPiecePosition] = 0;
            children.Add(new SlidingPuzzleState(_cost + 1, newPieces));
        }

        if (zeroCoordinate.Item1 < 2) {
            var newPieces = (int[])_pieces.Clone();
            var movingPieceCoordinate = (zeroCoordinate.Item1 + 1, zeroCoordinate.Item2);
            var movingPiecePosition = movingPieceCoordinate.Item2 * 3 + movingPieceCoordinate.Item1;
            newPieces[zeroPosition] = newPieces[movingPiecePosition];
            newPieces[movingPiecePosition] = 0;
            children.Add(new SlidingPuzzleState(_cost + 1, newPieces));
        }

        return children;
    }

    public override int CompareTo(SlidingPuzzleState? other) {
        return other == null ? 1 : ExpectedCost.CompareTo(other.ExpectedCost);
    }

    public override bool Equals(SlidingPuzzleState? other) {
        return other != null && _pieces.SequenceEqual(other._pieces);
    }

    public override int GetHashCode() {
        return _pieces.GetHashCode();
    }
}