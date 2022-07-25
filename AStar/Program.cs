namespace AStar;

internal static class AStar {
    private static int Main() {
        var generator = new Random();
        int[] initialPieces;
        var objectivePieces = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 0 };

        do {
            initialPieces = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }.OrderBy(_ => generator.Next()).ToArray();
        } while (!Solvable(initialPieces, objectivePieces));

        // initialPieces = new[] { 5, 8, 1, 4, 7, 2, 6, 3, 0 };

        var initialState = new SlidingPuzzleState(0, initialPieces, objectivePieces);
        var objective = new SlidingPuzzleState(0, objectivePieces, objectivePieces);
        var solver = new PuzzleSolver<SlidingPuzzleState>(initialState, objective);
        var result = solver.Solve();

        if (result is null) {
            Console.WriteLine(" No possible solution :\n" + initialState);
            return 0;
        }

        foreach (var element in result) Console.WriteLine(string.Join(',', element.Pieces));

        return 0;
    }

    private static bool Solvable(IReadOnlyList<int> initialPieces, int[] objectivePieces) {
        var inversions = 0;

        for (var i = 0; i < 8; i++)
        for (var j = i + 1; j < 9; j++)
            if (initialPieces[i] != 0 && initialPieces[j] != 0 &&
                Array.IndexOf(objectivePieces, initialPieces[j]) < Array.IndexOf(objectivePieces, initialPieces[i]))
                inversions++;

        return inversions % 2 == 0;
    }
}