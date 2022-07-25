using System.Diagnostics.CodeAnalysis;

namespace AStar.AdHocCollections;

public partial class StateTree<T> {
    public T Pop() {
        if (_root is null)
            throw new InvalidOperationException("Popping an empty tree");

        var element = PopFrom(ref _root);
        Count--;
        _stateLookup.Remove(element);
        return element;
    }

    private static T PopFrom([DisallowNull] ref Node? node) {
        if (node.Left is null) {
            var data = node.Data;
            node = node.Right;
            return data;
        }

        var element = PopFrom(ref node.Left);

        Rebalance(ref node);

        return element;
    }
}