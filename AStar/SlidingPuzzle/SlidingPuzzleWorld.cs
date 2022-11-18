using AStar.BaseTypes;

namespace AStar.SlidingPuzzle;

public class SlidingPuzzleWorld : World<SlidingPuzzleState>
{
    public SlidingPuzzleWorld(SlidingPuzzleState objective) : base(objective) { }

    public override int Heuristics(SlidingPuzzleState state)
    {
        var sum = 0;
        // Sum of distances to objective location
        for (var i = 1; i < state.Pieces.Length; i++)
        {
            var objectiveCoordinate = Objective.CoordinateOf(i);
            var currentCoordinate = state.CoordinateOf(i);
            sum += Math.Abs(objectiveCoordinate.Item1 - currentCoordinate.Item1) +
                   Math.Abs(objectiveCoordinate.Item2 - currentCoordinate.Item2);
        }

        return sum;
    }
}