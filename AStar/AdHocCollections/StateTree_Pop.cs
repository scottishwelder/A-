namespace AStar.AdHocCollections;

public partial class StateTree<T> {
    public T Pop() {
        if (_root is null)
            throw new InvalidOperationException("Popping an empty tree");

        Count--;
        (var element, _root) = PopFrom(_root);
        return element;
    }

    private static (T, Node?) PopFrom(Node node) {
        if (node.Left is null)
            return (node.Data, node.Right);

        (var element, node.Left) = PopFrom(node.Left);
        var bf = GetHeight(node.Left) - GetHeight(node.Right);
        switch (bf) {
            case < -1:
                node = GetHeight(node.Right!.Right) > GetHeight(node.Right.Left)
                    ? RotateLeft(node)
                    : RotateRightLeft(node);
                break;
            case > 1:
                throw new Exception(
                    "A popping operation required a rotation from the left. Something is wrong.");
            default:
                node.UpdateHeight();
                break;
        }

        return (element, node);
    }
}