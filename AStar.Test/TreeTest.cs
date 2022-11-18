using AStar.AdHocCollections;
using Xunit.Abstractions;

namespace AStar.Test;

public class TreeTest {
    private static readonly double Phi = (1 + Math.Sqrt(5)) / 2;
    private readonly ITestOutputHelper _outputHelper;

    public TreeTest(ITestOutputHelper outputHelper) {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void AddTest() {
        var (tree, set) = GetTree(100_000);
        VerifyTree(tree, set, set.Count);
    }

    [Fact]
    public void PopTest() {
        var (tree, set) = GetTree(10_000);
        while (set.Count > 0) {
            int treeElement = tree.Pop();
            int setElement = set.First();
            set.Remove(setElement);
            Assert.Equal(setElement, treeElement);
            if (set.Count % 1000 == 0)
                VerifyTree(tree, set, set.Count);
        }

        VerifyTree(tree, Array.Empty<int>(), 0);
        Assert.Throws<InvalidOperationException>(() => tree.Pop());
    }

    [Fact]
    public void RemoveTest() {
        var (tree, set) = GetTree(100_000);
        var randomList = set.OrderBy(_ => new Random().Next()).ToList();
        int size = set.Count;
        for (var i = 0; i < size - 20; i++) {
            tree.Remove(randomList[0]);
            randomList.RemoveAt(0);
        }

        VerifyTree(tree, randomList.OrderBy(x => x), 20);
    }

    [Fact]
    public void StringTest() {
        var (tree, set) = GetTree(10_000);
        var expected = $"StateTree<System.Int32>[{string.Join(", \n", set)}]";
        Assert.Equal(expected, tree.ToString());
    }

    private static void VerifyTree<T>(StateTree<T> tree, IEnumerable<T> expectedContent, int expectedCount)
        where T : IComparable<T>, IEquatable<T> {
        Assert.Equal(expectedCount, tree.Count);

        double minHeight = Math.Log(tree.Count + 1, 2);
        double maxHeight = Math.Log(tree.Count + 2, Phi) + Math.Log(5, 2) / Math.Log(Phi, 2) / 2 - 2;
        Assert.InRange(tree.Height, minHeight, maxHeight);

        Assert.Equal(expectedContent, tree);
    }

    private static (StateTree<int>, SortedSet<int>) GetTree(int dataSize) {
        var tree = new StateTree<int>();
        var unsortedSet = Enumerable.Range(1, dataSize).Select(_ => new Random().Next()).ToHashSet();
        tree.Add(unsortedSet);
        var sortedSet = new SortedSet<int>(unsortedSet);
        return (tree, sortedSet);
    }
}