using AStar.AdHocCollections;
using Xunit.Abstractions;

namespace AStar.Test;

public class UnitTest1 {
    private static readonly double Phi = (1 + Math.Sqrt(5)) / 2;
    private readonly ITestOutputHelper _outputHelper;

    public UnitTest1(ITestOutputHelper outputHelper) {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void Test1() {
        var dataSize = 10_000_000;
        var generator = new Random();

        var data = Enumerable.Range(1, dataSize).Select(_ => generator.Next()).ToHashSet();
        var tree = new StateTree<int> { data };
        var set = new SortedSet<int>(data);
        dataSize = set.Count;

        VerifyTree(tree, set, dataSize);

        while (set.Count > dataSize / 2) {
            var treeElement = tree.Pop();
            var setElement = set.First();
            set.Remove(setElement);
            Assert.Equal(setElement, treeElement);
        }

        VerifyTree(tree, set, dataSize / 2);

        while (set.Count > 0) {
            var treeElement = tree.Pop();
            var setElement = set.First();
            set.Remove(setElement);
            Assert.Equal(setElement, treeElement);
        }

        VerifyTree(tree, Array.Empty<int>(), 0);
        Assert.Throws<InvalidOperationException>(() => tree.Pop());
        VerifyTree(tree, Array.Empty<int>(), 0);
    }

    private static void VerifyTree<T>(StateTree<T> tree, IEnumerable<T> expectedContent, int expectedCount)
        where T : IComparable<T> {
        Assert.Equal(expectedCount, tree.Count);
        var minHeight = Math.Log(tree.Count + 1, 2);
        var maxHeight = Math.Log(tree.Count + 2, Phi) + Math.Log(5, 2) / Math.Log(Phi, 2) / 2 - 2;
        Assert.True(maxHeight < 40);
        Assert.InRange(tree.Height, minHeight, maxHeight);
        Assert.Equal(expectedContent, tree);
    }
}