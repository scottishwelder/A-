namespace AStar;

internal static class AStar {
    private static int Main() {
        var generator = new Random();
        int[] initialPieces;
        bool solvable;

        do {
            initialPieces = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }.OrderBy(_ => generator.Next()).ToArray();

            solvable = Solvable(initialPieces);
        } while (!solvable);


        var objectivePieces = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
        var initialState = new SlidingPuzzleState(0, initialPieces, objectivePieces);
        var objective = new SlidingPuzzleState(0, objectivePieces, objectivePieces);
        var solver = new PuzzleSolver<SlidingPuzzleState>(initialState, objective);
        var result = solver.Solve();

        foreach (var element in result) Console.WriteLine(string.Join(',', element.Pieces));

        return 0;
    }

    private static bool Solvable(IReadOnlyList<int> initialPieces) {
        var inversions = 0;
        for (var i = 0; i < 8; i++)
        for (var j = i + 1; j < 9; j++)
            if (initialPieces[j] < initialPieces[i])
                inversions++;

        return inversions % 2 == 0;
    }
}