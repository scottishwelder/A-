namespace AStar;

public class SlidingPuzzleState : State<SlidingPuzzleState> {
    public readonly int[] Pieces;

    public SlidingPuzzleState(int cost, int heuristicCost, int[] pieces) : base(cost, heuristicCost) {
        Pieces = pieces;
    }

    public override int CompareTo(SlidingPuzzleState? other) {
        if (other is null) return 1;
        var compareExpected = ExpectedCost.CompareTo(other.ExpectedCost);
        return compareExpected == 0 ? HeuristicCost.CompareTo(other.HeuristicCost) : compareExpected;
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

    // return Pieces.GetHashCode();
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
        return $"SlidingPuzzleState {{[{string.Join(", ", Pieces)}], {ExpectedCost}, {HeuristicCost}}}";
    }
}