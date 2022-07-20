namespace AStar;

public class SlidingPuzzleState : State<SlidingPuzzleState> {
    private readonly int[] _objective;
    public readonly int[] Pieces;

    public SlidingPuzzleState(int cost, int[] pieces, int[] objective) : base(cost) {
        Pieces = pieces;
        _objective = objective;
        PostConstruction();
    }

    protected override int Heuristics() {
        var sum = 0;
        for (var i = 1; i < Pieces.Length; i++) {
            var objectiveIndex = Array.IndexOf(_objective, i);
            var objectivePosition = (objectiveIndex % 3, objectiveIndex / 3);
            var currentIndex = Array.IndexOf(Pieces, i);
            var currentPosition = (currentIndex % 3, currentIndex / 3);
            sum += Math.Abs(objectivePosition.Item1 - currentPosition.Item1) +
                   Math.Abs(objectivePosition.Item2 - currentPosition.Item2);
        }

        return sum;
    }

    //TODO Lower Redundancy
    public override IEnumerable<SlidingPuzzleState> Children() {
        var zeroPosition = Array.IndexOf(Pieces, 0);
        var zeroCoordinate = (zeroPosition % 3, zeroPosition / 3);

        var children = new List<SlidingPuzzleState>();

        if (zeroCoordinate.Item2 > 0) {
            var newPieces = (int[])Pieces.Clone();
            var movingPieceCoordinate = (zeroCoordinate.Item1, zeroCoordinate.Item2 - 1);
            var movingPiecePosition = movingPieceCoordinate.Item2 * 3 + movingPieceCoordinate.Item1;
            newPieces[zeroPosition] = newPieces[movingPiecePosition];
            newPieces[movingPiecePosition] = 0;
            children.Add(new SlidingPuzzleState(Cost + 1, newPieces, _objective));
        }

        if (zeroCoordinate.Item1 > 0) {
            var newPieces = (int[])Pieces.Clone();
            var movingPieceCoordinate = (zeroCoordinate.Item1 - 1, zeroCoordinate.Item2);
            var movingPiecePosition = movingPieceCoordinate.Item2 * 3 + movingPieceCoordinate.Item1;
            newPieces[zeroPosition] = newPieces[movingPiecePosition];
            newPieces[movingPiecePosition] = 0;
            children.Add(new SlidingPuzzleState(Cost + 1, newPieces, _objective));
        }

        if (zeroCoordinate.Item2 < 2) {
            var newPieces = (int[])Pieces.Clone();
            var movingPieceCoordinate = (zeroCoordinate.Item1, zeroCoordinate.Item2 + 1);
            var movingPiecePosition = movingPieceCoordinate.Item2 * 3 + movingPieceCoordinate.Item1;
            newPieces[zeroPosition] = newPieces[movingPiecePosition];
            newPieces[movingPiecePosition] = 0;
            children.Add(new SlidingPuzzleState(Cost + 1, newPieces, _objective));
        }

        if (zeroCoordinate.Item1 < 2) {
            var newPieces = (int[])Pieces.Clone();
            var movingPieceCoordinate = (zeroCoordinate.Item1 + 1, zeroCoordinate.Item2);
            var movingPiecePosition = movingPieceCoordinate.Item2 * 3 + movingPieceCoordinate.Item1;
            newPieces[zeroPosition] = newPieces[movingPiecePosition];
            newPieces[movingPiecePosition] = 0;
            children.Add(new SlidingPuzzleState(Cost + 1, newPieces, _objective));
        }

        return children;
    }

    public override int CompareTo(SlidingPuzzleState? other) {
        if (other == null) return 1;
        var compareExpected = ExpectedCost.CompareTo(other.ExpectedCost);
        return compareExpected == 0 ? HeuristicCost.CompareTo(other.HeuristicCost) : compareExpected;
    }

    public override bool Equals(SlidingPuzzleState? other) {
        if (other == null) return false;
        for (var i = 0; i < 9; i++) {
            if (Pieces[i] != other.Pieces[i]) return false;
        }

        return true;
        //return other != null && _pieces.SequenceEqual(other._pieces);
    }

    public override int GetHashCode() {
        return Pieces.GetHashCode();
    }
}