namespace AStar;

class AStar {
    private static int Main() {
        var initialState = new SlidingPuzzleState(1, new[] { 3, 6, 5, 8, 1, 7, 4, 2, 0 });
        var solver = new PuzzleSolver(initialState,
            new SlidingPuzzleState(0, new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }));
        var result = solver.Solve();
        Console.WriteLine(string.Join(',', result.Data._pieces));

        for (var node = result; node != null; node = node._parent) {
            Console.WriteLine(string.Join(',', node.Data._pieces));
        }

        return 0;
    }
}