namespace AStar;

internal static class AStar {
    private static int Main() {
        var initialPieces = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

        var generator = new Random();
        initialPieces = initialPieces.OrderBy(_ => generator.Next()).ToArray();

        var solvable = Solvable(initialPieces);
        if (!solvable) {
            Console.WriteLine("Not solvable");
            return -1;
        }

        var objectivePieces = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
        var initialState = new SlidingPuzzleState(0, initialPieces, objectivePieces);
        var objective = new SlidingPuzzleState(0, objectivePieces, objectivePieces);
        var solver = new PuzzleSolver(initialState, objective);
        var result = solver.Solve();

        for (var node = result; node != null; node = node._parent) {
            Console.WriteLine(string.Join(',', node.Data._pieces));
        }

        return 0;
    }

    private static bool Solvable(int[] initialPieces) {
        var inversions = 0;
        for (var i = 0; i < 8; i++) {
            for (var j = i + 1; j < 9; j++) {
                if (initialPieces[j] < initialPieces[i])
                    inversions++;
            }
        }

        return inversions % 2 == 0;
    }
}