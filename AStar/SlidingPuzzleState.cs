namespace AStar;

public class SlidingPuzzleState : State<SlidingPuzzleState> {
    public readonly int[] Pieces;

    public SlidingPuzzleState(int[] pieces) {
        Pieces = pieces;
    }

    public override IEnumerable<SlidingPuzzleState> Children() {
        var zeroPosition = Array.IndexOf(Pieces, 0);
        var zeroCoordinate = (zeroPosition % 3, zeroPosition / 3);

        var children = new List<SlidingPuzzleState>();

        SlidingPuzzleState GetNewState(int x, int y) {
            var newPieces = (int[])Pieces.Clone();
            var movingPiecePosition = y * 3 + x;
            newPieces[zeroPosition] = newPieces[movingPiecePosition];
            newPieces[movingPiecePosition] = 0;
            return new SlidingPuzzleState(newPieces);
        }

        if (zeroCoordinate.Item2 > 0) children.Add(GetNewState(zeroCoordinate.Item1, zeroCoordinate.Item2 - 1));

        if (zeroCoordinate.Item1 > 0) children.Add(GetNewState(zeroCoordinate.Item1 - 1, zeroCoordinate.Item2));

        if (zeroCoordinate.Item2 < 2) children.Add(GetNewState(zeroCoordinate.Item1, zeroCoordinate.Item2 + 1));

        if (zeroCoordinate.Item1 < 2) children.Add(GetNewState(zeroCoordinate.Item1 + 1, zeroCoordinate.Item2));

        return children;
    }

    public override bool Equals(SlidingPuzzleState? other) {
        return other is not null && Pieces.SequenceEqual(other.Pieces);
    }

    public override bool Equals(object? other) {
        return Equals(other as SlidingPuzzleState);
    }

    public override int GetHashCode() {
        return CombineHashCodes(Pieces.Select(x => x.GetHashCode()).ToArray());
    }

    private static int CombineHashCodes(params int[] hashCodes) {
        if (hashCodes is null) throw new ArgumentNullException(nameof(hashCodes));

        switch (hashCodes.Length) {
            case 0:
                throw new IndexOutOfRangeException();
            case 1:
                return hashCodes[0];
        }

        var result = hashCodes[0];

        for (var i = 1; i < hashCodes.Length; i++) result = HashCode.Combine(result, hashCodes[i]);
        // for (var i = 1; i < hashCodes.Length; i++) result = CombineHashCodes(result, hashCodes[i]);

        return result;
    }

    public override string ToString() {
        return $"SlidingPuzzleState[{string.Join(", ", Pieces)}]";
    }
}