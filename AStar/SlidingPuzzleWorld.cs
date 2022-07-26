namespace AStar;

public class SlidingPuzzleWorld : World<SlidingPuzzleState> {
    public SlidingPuzzleWorld(SlidingPuzzleState objective) : base(objective) { }

    protected override int Heuristics(SlidingPuzzleState state) {
        var sum = 0;
        for (var i = 1; i < state.Pieces.Length; i++) {
            var objectiveIndex = Array.IndexOf(Objective.Pieces, i);
            var objectivePosition = (objectiveIndex % 3, objectiveIndex / 3);
            var currentIndex = Array.IndexOf(state.Pieces, i);
            var currentPosition = (currentIndex % 3, currentIndex / 3);
            sum += Math.Abs(objectivePosition.Item1 - currentPosition.Item1) +
                   Math.Abs(objectivePosition.Item2 - currentPosition.Item2);
        }

        return sum;
    }

    //TODO Reduce Redundancy
    public override IEnumerable<SlidingPuzzleState> Children(SlidingPuzzleState state) {
        var zeroPosition = Array.IndexOf(state.Pieces, 0);
        var zeroCoordinate = (zeroPosition % 3, zeroPosition / 3);

        var children = new List<SlidingPuzzleState>();

        SlidingPuzzleState GetNewState(int x, int y) {
            var newPieces = (int[])state.Pieces.Clone();
            var movingPiecePosition = y * 3 + x;
            newPieces[zeroPosition] = newPieces[movingPiecePosition];
            newPieces[movingPiecePosition] = 0;
            return new SlidingPuzzleState(state.Cost + 1, newPieces);
        }

        if (zeroCoordinate.Item2 > 0) children.Add(GetNewState(zeroCoordinate.Item1, zeroCoordinate.Item2 - 1));

        if (zeroCoordinate.Item1 > 0) children.Add(GetNewState(zeroCoordinate.Item1 - 1, zeroCoordinate.Item2));

        if (zeroCoordinate.Item2 < 2) children.Add(GetNewState(zeroCoordinate.Item1, zeroCoordinate.Item2 + 1));

        if (zeroCoordinate.Item1 < 2) children.Add(GetNewState(zeroCoordinate.Item1 + 1, zeroCoordinate.Item2));

        return children;
    }
}