using System.Collections;
using AStar.SlidingPuzzle;

namespace AStar;

internal static class AStar
{
    private static int Main()
    {
        var objectivePieces = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
        var objectiveState = new SlidingPuzzleState(objectivePieces);
        int[] initialPieces = GetSolvableSlidingPuzzle(objectivePieces);
        // var initialPieces = new[] { 6, 3, 5, 4, 8, 1, 7, 2, 0 };
        var initialState = new SlidingPuzzleState(initialPieces);

        var world = new SlidingPuzzleWorld(objectiveState);
        var solver = new PuzzleSolver<SlidingPuzzleState>(initialState, world);
        var result = solver.Solve();

        if (result is not null)
            // Lists from solution to initial state
            foreach (var state in result)
                Console.WriteLine(state);
        else
            Console.WriteLine($"No possible solution for \n{initialState}");

        return 0;
    }

    private static bool IsSolvable(IReadOnlyList<int> initialPieces, int[] objectivePieces)
    {
        var inversions = 0;

        for (var i = 0; i < 8; i++)
        for (int j = i + 1; j < 9; j++)
            if (initialPieces[i] != 0 && initialPieces[j] != 0 &&
                Array.IndexOf(objectivePieces, initialPieces[j]) <
                Array.IndexOf(objectivePieces, initialPieces[i]))
                inversions++;

        return inversions % 2 == 0;
    }

    private static int[] GetSolvableSlidingPuzzle(int[] objectivePieces)
    {
        var generator = new Random();
        int[] initialPieces;
        do
        {
            initialPieces = Enumerable.Range(0, 9).OrderBy(_ => generator.Next()).ToArray();
        } while (!IsSolvable(initialPieces, objectivePieces));

        return initialPieces;
    }
}