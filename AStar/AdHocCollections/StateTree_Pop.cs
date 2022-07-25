namespace AStar.AdHocCollections;

public partial class StateTree<T> {
    public T Pop() {
        if (_root is null)
            throw new InvalidOperationException("Popping an empty tree");

        (var element, _root) = PopFrom(_root);
        Count--;
        _stateLookup.Remove(element);
        return element;
    }

    private static (T, Node?) PopFrom(Node node) {
        if (node.Left is null) return (node.Data, node.Right);

        (var element, node.Left) = PopFrom(node.Left);

        node = Rebalance(node);

        return (element, node);
    }
}