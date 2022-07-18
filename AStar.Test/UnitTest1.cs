using Xunit.Abstractions;

namespace AStar.Test;

public class UnitTest1 {
    private readonly ITestOutputHelper _outputHelper;
    public UnitTest1(ITestOutputHelper outputHelper) {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void Test1() {
        var state = new SlidingPuzzleState(5, new []{0,2,3,5,6,7,1,4,8});
        var children = state.Children();
        foreach (var child in children) {
            _outputHelper.WriteLine(string.Join(',', child._pieces));
        }
    }
}