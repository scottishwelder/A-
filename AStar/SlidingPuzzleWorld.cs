namespace AStar;

public class SlidingPuzzleWorld : World<SlidingPuzzleState> {
    public SlidingPuzzleWorld(SlidingPuzzleState objective) : base(objective) { }

    public override int Heuristics(SlidingPuzzleState state) {
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
}