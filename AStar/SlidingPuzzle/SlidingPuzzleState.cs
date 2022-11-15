using AStar.BaseTypes;

namespace AStar.SlidingPuzzle;

public class SlidingPuzzleState : State<SlidingPuzzleState> {
    public readonly int[] Pieces;

    public SlidingPuzzleState(int[] pieces) {
        Pieces = pieces;
    }

    public int PositionOf(int element) => Array.IndexOf(Pieces, element);

    public (int, int) CoordinateOf(int element) {
        var position = PositionOf(element);
        return (position % 3, position / 3);
    }

    public override IEnumerable<SlidingPuzzleState> Children() {
        var zeroPosition = PositionOf(0);
        var zeroCoordinate = CoordinateOf(0);

        SlidingPuzzleState GetNewStateMoving(int x, int y) {
            var newPieces = (int[])Pieces.Clone();
            var movingPiecePosition = y * 3 + x;
            newPieces[zeroPosition] = newPieces[movingPiecePosition];
            newPieces[movingPiecePosition] = 0;
            return new SlidingPuzzleState(newPieces);
        }

        if (zeroCoordinate.Item1 > 0) yield return GetNewStateMoving(zeroCoordinate.Item1 - 1, zeroCoordinate.Item2);
        if (zeroCoordinate.Item1 < 2) yield return GetNewStateMoving(zeroCoordinate.Item1 + 1, zeroCoordinate.Item2);
        if (zeroCoordinate.Item2 > 0) yield return GetNewStateMoving(zeroCoordinate.Item1, zeroCoordinate.Item2 - 1);
        if (zeroCoordinate.Item2 < 2) yield return GetNewStateMoving(zeroCoordinate.Item1, zeroCoordinate.Item2 + 1);
    }

    public override bool Equals(SlidingPuzzleState? other) => other is not null && Pieces.SequenceEqual(other.Pieces);
    public override string ToString() => $"SlidingPuzzleState[{string.Join(", ", Pieces)}]";
    public override int GetHashCode() => Pieces.Aggregate(0, HashCode.Combine);
}