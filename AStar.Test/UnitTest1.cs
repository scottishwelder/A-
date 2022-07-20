using AStar.AdHocCollections;
using Xunit.Abstractions;

namespace AStar.Test;

public class UnitTest1 {
    private readonly ITestOutputHelper _outputHelper;

    public UnitTest1(ITestOutputHelper outputHelper) {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void Test1() {
        var tree = new StateTree<int>();
        var generator = new Random();
        const int dataSize = 1_000_00;
        var data = Enumerable.Range(1, dataSize).Select(_ => generator.Next()).ToArray();

        tree.Add(data);
        _outputHelper.WriteLine(tree.Height.ToString());
        Array.Sort(data);
        Assert.True(data.SequenceEqual(tree));
        //_outputHelper.WriteLine(tree.ToString());
    }
}